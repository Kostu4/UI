using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Compass : MonoBehaviour
{
    [SerializeField] private GameObject iconPrefab; //For adding quest
    [SerializeField] private RawImage compassImage; //Compass direction
    [SerializeField] private Transform player; //player direcition
    [SerializeField] private float maxDistance = 200f; //for changing icons alpha
    [SerializeField] private QuestMarker[] questMarkersArray; // quests check
    private List<QuestMarker> questMarkers = new List<QuestMarker>(); //also quests
    private HealthBar healthBar; //For coroutine
    private float compassUnit; //compass rotation

    private void Awake()
    {
        healthBar = FindObjectOfType<HealthBar>();
        compassUnit = compassImage.rectTransform.rect.width / 360f;

        foreach (var marker in questMarkersArray)
        {
            AddQuestMarker(marker);
        }

        StartCoroutine(CompassTracker());
    }

    private IEnumerator CompassTracker()
    {
        while (healthBar.isDead==false)
        {
            UpdateCompass();
            yield return null;
        }
    }

    //Rotation Compass
    private void UpdateCompass()
    {
        compassImage.uvRect = new Rect(player.localEulerAngles.y / 360f, 0f, 1f, 1f);

        foreach (QuestMarker marker in questMarkers)
        {
            UpdateMarkerPosition(marker);
            UpdateMarkerAlpha(marker);
        }
    }

    //Updating markers positions on compass
    private void UpdateMarkerPosition(QuestMarker marker)
    {
        Vector2 posOnCompass = GetPosOnCompass(marker);
        marker.image.rectTransform.anchoredPosition = posOnCompass;
    }

    //When player is coming closer to "quest" marker will be brighter
    private void UpdateMarkerAlpha(QuestMarker marker)
    {
        float distance = Vector2.Distance(new Vector2(player.transform.position.x, player.transform.position.z), marker.position);
        float alpha = Mathf.Clamp(1 - (distance / maxDistance), 0.65f, 1f);
        Color color = marker.image.color;
        color.a = alpha;
        marker.image.color = color;
    }

    //Add quest
    public void AddQuestMarker(QuestMarker marker)
    {
        GameObject newMarker = Instantiate(iconPrefab, compassImage.transform);
        var imageComponent = newMarker.GetComponent<Image>();
        imageComponent.sprite = marker.icon;
        marker.image = imageComponent;

        questMarkers.Add(marker);
    }

    //Calculate the marker position to current player direction
    private Vector2 GetPosOnCompass(QuestMarker marker)
    {
        Vector2 playerPos = new Vector2(player.transform.position.x, player.transform.position.z);
        Vector2 playerFwd = new Vector2(player.transform.forward.x, player.transform.forward.z);

        float angle = Vector2.SignedAngle(marker.position - playerPos, playerFwd);

        return new Vector2(compassUnit * angle, 0f);
    }
}