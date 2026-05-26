using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class DropRateManager : MonoBehaviour
{
    [System.Serializable]
   public class Drops
    {
        public string name;
        public GameObject itemPrefab;
        public float dropRate;
    }
    public List<Drops> drops;

    void OnDestroy()
    {
        if (!gameObject.scene.isLoaded)
        {
            return;
        }
        List<Drops> possibleDrops = new List<Drops>();
        float randomNumber = UnityEngine.Random.Range(0f, 100f);

        foreach (Drops rate in drops)
        {
            if (randomNumber <= rate.dropRate)
            {
                possibleDrops.Add(rate);
            }
        }
        if (possibleDrops.Count > 0)
        {
            // Chọn ngẫu nhiên 1 trong số các món đồ đủ điều kiện rơi
            Drops selectedDrop = possibleDrops[UnityEngine.Random.Range(0, possibleDrops.Count)];
            Instantiate(selectedDrop.itemPrefab, transform.position, Quaternion.identity);
        }
        //    if (possibleDrops.Count > 0)
        //    {
        //        Drops drops = possibleDrops[UnityEngine.Random.Range(0, possibleDrops.Count)];
        //        Instantiate(rate.itemPrefab, transform.position, Quaternion.identity);
        //    }

        //}
    }


}
