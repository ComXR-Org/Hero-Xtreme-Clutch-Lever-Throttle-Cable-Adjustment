using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Video;

[AddComponentMenu("Networking/Download & Play (Video or Audio)")]
public class DownloadAndPlayMedia : MonoBehaviour
{
    [Header("Remote source")]
    [Tooltip("Full HTTP/HTTPS URL of the media file (.mp4, .mp3, .wav, .ogg …)")]
    [SerializeField] private string remoteUrl = "https://cdn.site/intro.mp4";

    [Header("Local cache")]
    [Tooltip("File name to save inside Application.persistentDataPath (include extension)")]
    [SerializeField] private string localFileName = "intro.mp4";

    [Header("Behaviour")]
    [SerializeField] private bool autoPlay = true;
    [SerializeField] private bool forceRedownload = false;

    /* ---------- Cached refs ----------- */
    public VideoPlayer vp;
    public AudioSource au;
    private string localPath;    // Absolute path on disk
    private string localUrl;     // "file://<path>" form

    /* ====================== Awake ====================== */
    private void Awake()
    {
        // Detect which component is present
        //TryGetComponent(out vp);
        //TryGetComponent(out au);

        if (vp == null && au == null)
        {
            Debug.LogError("[DownloadAndPlayMedia] Needs either VideoPlayer or AudioSource on the same GameObject.");
            enabled = false;
            return;
        }

        localPath = Path.Combine(Application.persistentDataPath, localFileName);
        localUrl = "file://" + localPath;

        StartCoroutine(DownloadIfNeededThenPlay());
    }

    /* ================== Download & Play ================ */
    private System.Collections.IEnumerator DownloadIfNeededThenPlay()
    {
        // 1. Download if file not cached (or forceRedownload)
        if (forceRedownload || !File.Exists(localPath))
        {
            using var req = UnityWebRequest.Get(remoteUrl);
            req.downloadHandler = new DownloadHandlerFile(localPath);
            yield return req.SendWebRequest();

            if (req.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"[DownloadAndPlayMedia] Download failed: {req.error}");
                yield break;
            }
        }

        // 2. Decide how to play
        if (vp != null)                       // ---------- VIDEO ----------
        {
            vp.source = VideoSource.Url;
            vp.playOnAwake = true;
            vp.url = localUrl;
            vp.Prepare();

            while (!vp.isPrepared) yield return null;
            if (autoPlay) vp.Play();
        }
        else if (au != null)                  // ---------- AUDIO ----------
        {
            var audioType = GuessAudioType(localPath);
            if (audioType == AudioType.UNKNOWN)
            {
                Debug.LogError($"[DownloadAndPlayMedia] Unknown audio extension on {localPath}");
                yield break;
            }

            using var req = UnityWebRequestMultimedia.GetAudioClip(localUrl, audioType);
            yield return req.SendWebRequest();

            if (req.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"[DownloadAndPlayMedia] AudioClip load failed: {req.error}");
                yield break;
            }

            au.clip = DownloadHandlerAudioClip.GetContent(req);
            if (autoPlay) au.Play();
        }
    }

    /* ------------- Helpers ----------------------------- */
    private static AudioType GuessAudioType(string path)
    {
        string ext = Path.GetExtension(path).ToLowerInvariant();
        return ext switch
        {
            ".mp3" => AudioType.MPEG,
            ".wav" => AudioType.WAV,
            ".ogg" => AudioType.OGGVORBIS,
            ".aiff" or ".aif" => AudioType.AIFF,
            ".aac" => AudioType.ACC,
            _ => AudioType.UNKNOWN
        };
    }

    /* ------------- Public utilities -------------------- */
    public bool IsCached() => File.Exists(localPath);
    public string LocalFilePath() => localPath;
    public void DeleteCachedCopy() { if (File.Exists(localPath)) File.Delete(localPath); }
}
