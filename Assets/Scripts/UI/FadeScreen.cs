using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class FadeScreen : MonoBehaviour
{
    private Image _img;
    private Coroutine _coroutineFade;

    void Awake()
    {
        _img = GetComponent<Image>();
    }

    public void FadeToOpaque(float dTDuration = 2.0f)
    {
        gameObject.SetActive(true);
        FadeOverTime(1.0f, dTDuration);
    }

    public void FadeToTransparent(float dTDuration = 2.0f)
    {
        FadeOverTime(0.0f, dTDuration);
    }

    public void SnapToOpaque()
    {
        gameObject.SetActive(true);
        _img.color = new Color(_img.color.r, _img.color.g, _img.color.b, 1.0f);
    }

    public void SnapToTransparent()
    {
        _img.color = new Color(_img.color.r, _img.color.g, _img.color.b, 0.0f);
        gameObject.SetActive(false);
    }

    public bool IsFading()
    {
        return (_coroutineFade != null);
    }

    private void FadeOverTime(float alphaFinal, float dTDuration)
    {
        if (_coroutineFade != null)
            StopCoroutine(_coroutineFade);

        _coroutineFade = StartCoroutine(Fade(alphaFinal, dTDuration));
    }

    private IEnumerator Fade(float alphaFinal, float dTDuration)
    {
        float start = Time.time;

        // NOTE (bobbyz) Apparently lerping doesn't quite reach 0, so adding a timeout on top

        while (_img.color.a != alphaFinal && Time.time < start + dTDuration + 0.5f)
        {
            float elapsed = Time.time - start;
            float normalisedTime = Mathf.Clamp((elapsed / dTDuration) * Time.deltaTime, 0, 1);
            float alphaCur = Mathf.Lerp(_img.color.a, alphaFinal, normalisedTime);
            _img.color = new Color(_img.color.r, _img.color.g, _img.color.b, alphaCur);
            yield return 0;
        }

        if (alphaFinal == 0.0f)
        {
            gameObject.SetActive(false);
        }

        yield return true;
    }
}
