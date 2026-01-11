using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DealParserPanel : MonoBehaviour
{
    [SerializeField]
    private GameObject mainPanel;

    [SerializeField, ReadOnlyInspector]
    private ScrollRect uiScroll;

    [SerializeField, ReadOnlyInspector]
    private List<DealParserElemnt> listElements;

    [SerializeField, ReadOnlyInspector]
    private int iTotalDamage;
    [SerializeField]
    private TextMeshProUGUI txtTotalDmg;
    [SerializeField, ReadOnlyInspector]
    private int iFirstDmg;
    [SerializeField]
    private List<Sprite> listHeroFrame;

    [SerializeField, ReadOnlyInspector]
    private List<RectTransform> listOrgYList;

    private Coroutine smoothCoroutine;

    private void Awake()
    {
        if (uiScroll == null || listElements == null || listElements.Count == 0)
            Init();
    }

    [ContextMenu("Init")]
    private void Init()
    {
        uiScroll = GetComponentInChildren<ScrollRect>();

        listElements = new List<DealParserElemnt>();
        listOrgYList = new List<RectTransform>();

        if (uiScroll == null || uiScroll.content == null)
            return;

        for (int i = 0; i < uiScroll.content.childCount; i++)
        {
            var elementTransform = uiScroll.content.GetChild(i);
            if (elementTransform == null)
                continue;

            var element = elementTransform.GetComponent<DealParserElemnt>();
            if (element != null)
                listElements.Add(element);

            var rect = elementTransform.GetComponent<RectTransform>();
            if (rect != null)
                listOrgYList.Add(rect);
        }
    }

    void Start()
    {
        if (uiScroll == null || listElements == null || listElements.Count == 0)
            Init();

        UpdateTotalDamage();

        if (listElements != null)
        {
            foreach (var item in listElements)
            {
                if (item != null)
                    item.gameObject.SetActive(false);
            }
        }

        if (InGameManager.instance != null && InGameManager.instance.status == StageStatus.Running)
            UpdateHeroList();
    }

    private void OnEnable()
    {
        if (smoothCoroutine == null)
            smoothCoroutine = StartCoroutine(SmoothUIUpdateCoroutine());
    }

    private void OnDisable()
    {
        if (smoothCoroutine != null)
        {
            StopCoroutine(smoothCoroutine);
            smoothCoroutine = null;
        }
    }

    public void OnOffPanel(bool _bOn)
    {
        if (mainPanel != null)
            mainPanel.gameObject.SetActive(_bOn);
    }

    IEnumerator SmoothUIUpdateCoroutine()
    {
        while (true)
        {
            if (listElements != null)
            {
                for (int i = 0; i < listElements.Count; i++)
                {
                    var item = listElements[i];
                    if (item == null || !item.gameObject.activeSelf)
                        continue;

                    if (item.display == null)
                        continue;

                    int targetRank = item.rank;
                    if (targetRank < 0 || targetRank >= listElements.Count)
                        continue;

                    var target = listElements[targetRank];
                    if (target == null)
                        continue;

                    item.display.position = Vector3.Lerp(item.display.position, target.transform.position, 0.25f);
                }
            }

            yield return null;
        }
    }

    public void UpdateHeroList()
    {
        iTotalDamage = 0;
        UpdateTotalDamage();

        if (listElements == null || listElements.Count == 0)
            return;

        if (InGameManager.instance == null || InGameManager.instance.cPlayerController == null || InGameManager.instance.cPlayerController.placedHeros == null)
        {
            for (int i = 0; i < listElements.Count; i++)
                listElements[i].gameObject.SetActive(false);
            return;
        }

        int _iActiveCount = 0;

        List<Hero> _listHero = InGameManager.instance.cPlayerController.placedHeros
            .OrderByDescending(x => x.unitLevel)
            .ThenByDescending(x => x.heroCost)
            .ToList();

        int count = Mathf.Min(_listHero.Count, listElements.Count);

        for (int i = 0; i < count; i++)
        {
            listElements[i].InitHero(_listHero[i]);
            listElements[i].gameObject.SetActive(true);
            listElements[i].SetRank(_iActiveCount);
            _iActiveCount++;
        }

        for (int i = _iActiveCount; i < listElements.Count; i++)
            listElements[i].gameObject.SetActive(false);
    }

    public void AddTotalDamage(int _iDamage)
    {
        iTotalDamage += _iDamage;
        UpdateTotalDamage();
    }

    private void UpdateTotalDamage()
    {
        if (txtTotalDmg != null)
            txtTotalDmg.text = iTotalDamage.ToString();
    }

    public Sprite GetHeroFrameSprite(int _iLevel)
    {
        if (listHeroFrame == null || listHeroFrame.Count == 0)
            return null;

        int idx = Mathf.Clamp(_iLevel - 1, 0, listHeroFrame.Count - 1);
        return listHeroFrame[idx];
    }

    public void UpdateAllUnit()
    {
        if (listElements == null || listElements.Count == 0)
        {
            iFirstDmg = 0;
            return;
        }

        List<DealParserElemnt> _listTemp = new List<DealParserElemnt>();

        foreach (var item in listElements)
        {
            if (item == null || !item.gameObject.activeSelf)
                continue;

            _listTemp.Add(item);
        }

        if (_listTemp.Count == 0)
        {
            iFirstDmg = 0;
            return;
        }

        _listTemp.Sort((x, y) => y.totalDamage.CompareTo(x.totalDamage));

        iFirstDmg = (int)_listTemp[0].totalDamage;

        for (int i = 0; i < _listTemp.Count; i++)
        {
            _listTemp[i].UpdateDamage();
            _listTemp[i].SetRank(i);
        }
    }

    public int totalDamage { get { return iTotalDamage; } }
    public int firstDamage { get { return iFirstDmg; } }
}
