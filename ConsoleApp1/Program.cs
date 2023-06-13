namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            






        }
        //冒泡排序
        static void BubbleSort(int[] arr)
        {
            int temp = 0;
            for (int i = 0; i < arr.Length-1; i++)
            {
                for (int j = 0; j < arr.Length-1-i; j++)
                {
                    if (arr[j] > arr[j+1])
                    {
                        temp = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = temp;
                    }
                }
            }
        }

        //quickSort
        static void QuickSort(int[] arr,int left,int right)
        {
            if (left > right)
            {
                return;
            }
            int i = left;
            int j = right;
            int temp = arr[left];
            while (i != j)
            {
                while (arr[j] >= temp && i < j)
                {
                    j--;
                }
                while (arr[i] <= temp && i < j)
                {
                    i++;
                }
                if (i < j)
                {
                    int t = arr[i];
                    arr[i] = arr[j];
                    arr[j] = t;
                }
            }
            arr[left] = arr[i];
            arr[i] = temp;
            QuickSort(arr, left, i - 1);
            QuickSort(arr, i + 1, right);
        }



        //use httpclient downlaod url
        static async Task<string> GetHtmlAsync(string url)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                return await response.Content.ReadAsStringAsync();
            }
        }

        //send email
        static void SendEmail()
        {

        }

        //校验身份证

      
    
    }
}