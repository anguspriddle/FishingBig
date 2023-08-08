using System.Collections;
using UnityEngine;

public class SkillCheckManager : MonoBehaviour
{
    public static SkillCheckManager Instance;

    public GameObject skillBlock;
    public GameObject skillCheckArea;
    public GameObject skillCheckPoint;

    private bool skillCheckActive;

    private FishingManager fishingManager;
    private bool skillCheckSuccess;

    private void Awake()
    {
        Instance = this;
    }

    public void StartSkillCheck(FishingManager manager)
    {
        fishingManager = manager;
        skillCheckSuccess = false;
        StartCoroutine(SkillCheckProcess());
    }

    private IEnumerator SkillCheckProcess()
    {
        float skillCheckX = Random.Range(skillBlock.transform.position.x - skillBlock.transform.localScale.x / 2f,
                                         skillBlock.transform.position.x + skillBlock.transform.localScale.x / 2f);
        skillCheckArea.transform.position = new Vector3(skillCheckX, skillCheckArea.transform.position.y, skillCheckArea.transform.position.z);

        skillCheckActive = true;
        skillCheckArea.SetActive(true);
        skillCheckPoint.SetActive(true);

        while (skillCheckActive)
        {
            // Move the skillCheckPoint from left to right.

            if (Input.GetKeyDown(KeyCode.Space) && IsPointInsideSkillCheckArea(skillCheckPoint.transform.position))
            {
                skillCheckActive = false;
                skillCheckPoint.SetActive(false);
                skillCheckArea.SetActive(false);
                skillCheckSuccess = true;

                // Skill check success!
                Fish caughtFish = fishingManager.DetermineCaughtFish();
                fishingManager.EndFishing(skillCheckSuccess, caughtFish);
            }

            yield return null;
        }
    }

    private bool IsPointInsideSkillCheckArea(Vector3 point)
    {
        Vector3 skillCheckAreaLeft = skillCheckArea.transform.position - skillCheckArea.transform.localScale / 2f;
        Vector3 skillCheckAreaRight = skillCheckArea.transform.position + skillCheckArea.transform.localScale / 2f;

        return point.x >= skillCheckAreaLeft.x && point.x <= skillCheckAreaRight.x;
    }
}
