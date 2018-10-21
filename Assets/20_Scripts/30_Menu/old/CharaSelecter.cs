//using UnityEngine;

//public class CharaSelecter : MonoBehaviour
//{
//    private static readonly int CharaNum = 3;

//    // GameObject selecter1;
//    // GameObject selecter2;
//    // GameObject selecter3;
//    // GameObject selecter4;
//    // GameObject selecter5;
//    // GameObject selecter6;
//    [SerializeField]
//    private GameObject[] pl1Selectors = new GameObject[0];
//    [SerializeField]
//    private GameObject[] pl2Selectors = new GameObject[0];
//    private int pl1Index = 0;
//    private int pl2Index = 0;
    
//    private void Start()
//    {
//        // selecter1 = GameObject.FindWithTag("selecter1");
//        // selecter2 = GameObject.FindWithTag("selecter2");
//        // selecter3 = GameObject.FindWithTag("selecter3");
//        // selecter4 = GameObject.FindWithTag("selecter4");
//        // selecter5 = GameObject.FindWithTag("selecter5");
//        // selecter6 = GameObject.FindWithTag("selecter6");
//        // selecter2.SetActive(false);
//        // selecter3.SetActive(false);
//        // selecter5.SetActive(false);
//        // selecter6.SetActive(false);
//        for (int i = 0 + 1; i < this.pl1Selectors.Length; ++i)
//        {
//            this.pl1Selectors[i].SetActive(false);
//        }
//        for (int i = 0 + 1; i < this.pl2Selectors.Length; ++i)
//        {
//            this.pl2Selectors[i].SetActive(false);
//        }
//    }
    
//    private void Update()
//    {   
//        // 1Pのキー設定
//        if (Input.GetKeyDown(KeyCode.S))
//        {
//            this.pl1Index++;
//            this.Buffer1P(this.pl1Index);
//        }

//        if (Input.GetKeyDown(KeyCode.W))
//        {
//            this.pl1Index--;
//            this.Buffer1P(this.pl1Index);
//        }

//        if (Input.GetKeyDown(KeyCode.Space))
//        {
//            // if (pl1Selectors[0].activeSelf) print("1P-chara1");
//            // else if (pl1Selectors[1].activeSelf) print("1P-chara2");
//            // else if (pl1Selectors[2].activeSelf) print("1P-chara3");
//            // else print("これがでたら終末");

//            bool errCheckFlg = false;

//            for (int i = 0; i < this.pl1Selectors.Length / 2; ++i)
//            {
//                if (this.pl1Selectors[i].activeSelf)
//                {
//                    if (errCheckFlg) { Debug.LogError("これが出たら終末", this); }

//                    errCheckFlg = true;
//                    Debug.Log("1P-chara" + (i + 1), this.pl1Selectors[i]);
//                }
//            }
//        }

//        // 2Pのキー設定
//        if (Input.GetKeyDown(KeyCode.K))
//        {
//            this.pl2Index++;
//            print(this.pl2Index);
//            this.Buffer2P(this.pl2Index);
//        }

//        if (Input.GetKeyDown(KeyCode.I))
//        {
//            this.pl2Index--;
//            print(this.pl2Index);
//            this.Buffer2P(this.pl2Index);
//        }

//        if (Input.GetKeyDown(KeyCode.Return))
//        {
//            // if (pl2Selectors[3].activeSelf) print("2P-chara1");
//            // else if (pl2Selectors[4].activeSelf) print("2P-chara2");
//            // else if (pl2Selectors[5].activeSelf) print("2P-chara3");
//            // else print("これがでたら終末");

//            bool errCheckFlg = false;

//            for (int i = 0; i < pl2Selectors.Length; ++i)
//            {
//                if (this.pl1Selectors[i].activeSelf)
//                {
//                    if (errCheckFlg) { Debug.LogError("これが出たら終末", this); }

//                    errCheckFlg = true;
//                    Debug.Log("2P-chara" + (i + 1), this.pl2Selectors[i]);
//                }
//            }
//        }

//    }

//    private void Buffer1P(int _counts)
//    {
//        // switch (_counts % 3)
//        // {
//        //     case 0:
//        //         pl1Selectors[0].SetActive(true);
//        //         pl1Selectors[1].SetActive(false);
//        //         pl1Selectors[2].SetActive(false);
//        //         break;
//        //     case 1:
//        //         pl1Selectors[0].SetActive(false);
//        //         pl1Selectors[1].SetActive(true);
//        //         pl1Selectors[2].SetActive(false);
//        //         break;
//        //     case 2:
//        //         pl1Selectors[0].SetActive(false);
//        //         pl1Selectors[1].SetActive(false);
//        //         pl1Selectors[2].SetActive(true);
//        //         break;
//        // };

//        for (int i = 0; i < pl1Selectors.Length; ++i)
//        { 
//            pl1Selectors[i].SetActive(false);
//        }

//        pl1Selectors[_counts % CharaNum].SetActive(true);
//    }

//    private void Buffer2P(int _counts)
//    {
//        // switch (_counts % 3)
//        // {
//        //     case 0:
//        //         pl2Selectors[0].SetActive(true);
//        //         pl2Selectors[1].SetActive(false);
//        //         pl2Selectors[2].SetActive(false);
//        //         break;
//        //     case 1:
//        //         pl2Selectors[0].SetActive(false);
//        //         pl2Selectors[1].SetActive(true);
//        //         pl2Selectors[2].SetActive(false);
//        //         break;
//        //     case 2:
//        //         pl2Selectors[0].SetActive(false);
//        //         pl2Selectors[1].SetActive(false);
//        //         pl2Selectors[2].SetActive(true);
//        //         break;
//        // };
        
//        for (int i = 0; i < pl2Selectors.Length; ++i)
//        {
//            pl2Selectors[i].SetActive(false);
//        }

//        pl2Selectors[_counts % CharaNum].SetActive(true);
//    }
//}