using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;

public class TutorialPanel : Panel
{
    [Header("Reveal Settings")]
    [SerializeField] private float revealButton = 2f; // Jeda waktu sebelum tombol muncul (detik)
    [SerializeField] private float speedReveal = 0.5f; // Kecepatan durasi animasi fade
    
    [Header("Panel Elements")]
    [SerializeField] private CanvasGroup panelCanvasGroup;
    [SerializeField] private CanvasGroup contentCanvasGroup;
    
    [Header("Button Reveal Element")]
    [SerializeField] private CanvasGroup buttonCanvasGroup; // Tambahkan CanvasGroup tombol di Inspector
    [SerializeField] private RectTransform buttonRectTransform; // Opsional: Untuk efek scale up

    private void Start()
    {
        // Pastikan DOTween sudah di-inisialisasi (opsional tapi baik untuk best practice)
        DOTween.Init();
    }

    public override IEnumerator AnimationOpenPanel()
    {
        // 1. Setup Awal: Sembunyikan semua elemen saat panel pertama terbuka
        panelCanvasGroup.alpha = 0f;
        
        buttonCanvasGroup.alpha = 0f;
        buttonCanvasGroup.interactable = false;
        buttonCanvasGroup.blocksRaycasts = false;
        
        if (buttonRectTransform != null)
        {
            buttonRectTransform.localScale = Vector3.zero; // Set ukuran ke 0 biar ada efek pop-up
        }

        // 2. Fade in background/frame panel terlebih dahulu
        yield return panelCanvasGroup.DOFade(1f, speedReveal).WaitForCompletion();
        
        panelCanvasGroup.interactable = true;
        panelCanvasGroup.blocksRaycasts = true;
        contentCanvasGroup.interactable = true;
        contentCanvasGroup.blocksRaycasts = true;
        
        // 3. Fade in content utama
        yield return contentCanvasGroup.DOFade(1f, speedReveal).WaitForCompletion();

        // 4. JEDA TIMING REVEAL BUTTON
        // Menunggu selama beberapa detik (sesuai variabel revealButton) sebelum tombol muncul
        yield return new WaitForSeconds(revealButton);

        // 5. ANIMASI REVEAL TOMBOL (Fade + Pop Up Scale)
        buttonCanvasGroup.interactable = true;
        buttonCanvasGroup.blocksRaycasts = true;
        
        // Menjalankan animasi fade-in tombol
        buttonCanvasGroup.DOFade(1f, speedReveal);
        
        if (buttonRectTransform != null)
        {
            // Efek pop-up membesar dengan efek sedikit membal (Ease.OutBack)
            yield return buttonRectTransform.DOScale(Vector3.one, speedReveal).SetEase(Ease.OutBack).WaitForCompletion();
        }
        else
        {
            yield return buttonCanvasGroup.DOFade(1f, speedReveal).WaitForCompletion();
        }
    }

    public override IEnumerator AnimationClosePanel()
    {
        // Saat panel ditutup, langsung hilangkan interaksi tombol
        buttonCanvasGroup.interactable = false;
        buttonCanvasGroup.blocksRaycasts = false;

        // Fade out tombol dan content secara bersamaan
        buttonCanvasGroup.DOFade(0f, speedReveal);
        if (buttonRectTransform != null) buttonRectTransform.DOScale(Vector3.zero, speedReveal);
        
        yield return contentCanvasGroup.DOFade(0f, speedReveal).WaitForCompletion();

        panelCanvasGroup.interactable = false;
        panelCanvasGroup.blocksRaycasts = false;
        contentCanvasGroup.interactable = false;
        contentCanvasGroup.blocksRaycasts = false;
        
        // Terakhir, fade out panel background-nya
        yield return panelCanvasGroup.DOFade(0f, speedReveal).WaitForCompletion();
    }
}
