using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArtifactSlot : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] Button _removeButton;
    [SerializeField] Sprite _defaultSprite;
    [SerializeField] Image _removeButtonImage;

    public ArtifactItem Artifact { get; private set; }

    private void OnEnable()
    {
        _removeButton.onClick.AddListener(RemoveArtifact);
    }

    private void OnDisable()
    {
        _removeButton.onClick.RemoveListener(RemoveArtifact);
    }

    private void RemoveArtifact()
    {
        if (Artifact)
        {
            _player.RemoveArtifact(Artifact);
            _removeButtonImage.sprite = _defaultSprite;
            Artifact = null;
        }
    }

    public void AddArtifact(ArtifactItem artifact)
    {
        Artifact = artifact;
        _removeButtonImage.sprite = artifact.SpriteImage;
    }
}
