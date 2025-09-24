namespace _25_KT3
{
    internal static class PackagingUnpackaging
    {
        public static float GetSumOfObjectList(List<object> list)
        {
            float sum = 0;
            try
            {
                for (int i = 0; i < list.Count; i++)
                {
                    sum += Convert.ToSingle(list[i]);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return sum;
        }
    }
}
