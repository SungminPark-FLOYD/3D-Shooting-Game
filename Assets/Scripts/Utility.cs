
using System.Collections;



public class Utility 
{
    //seed : 랜덤값을 만드는데 기준이 되는 초기값
    public static T[] ShuffleArray<T>(T[] array, int seed)
    {
        System.Random prng = new System.Random(seed);

        //마지막 루프 생략
        for(int i =0; i < array.Length -1; i++)
        {
            int randomIndex = prng.Next(i, array.Length);
            T tempItem = array[randomIndex];
            array[randomIndex] = array[i];
            array[i] = tempItem;
        }

        return array;
    }
}
