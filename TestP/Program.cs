using Newtonsoft.Json;

namespace TestP;

class Program

{
    static void Main(string[] args)
    {
        JsonToTxt();
       
        System.Diagnostics.Process proc = new System.Diagnostics.Process();
        proc.StartInfo.FileName = "SIV_03.exe";
        proc.Start();
        proc.WaitForExit();

        TxtToJson();
        
        var subOut = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
        string out_path = @"OutputFiles";
        string subpath = subOut;
        DirectoryInfo dirInfo = new DirectoryInfo(out_path);
        if (!dirInfo.Exists)
        {
            dirInfo.Create();
        }
        dirInfo.CreateSubdirectory(subpath);
        string[] files= {"file_in.txt","file_out.txt","file_DE_out.txt","output.json"};
        for (int i = 0; i < files.Length; i++)
        {
            string destinationFile = Path.Combine(Path.Combine(out_path,subpath), files[i]);
            File.Move(files[i], destinationFile);
        }
    }

    private static void JsonToTxt()
    {
        using (StreamReader sr = new StreamReader("main_input.json"))
        {
            // Десериализация файла json в список объектов
            List<Dictionary<string, string>> data = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(sr.ReadToEnd());

            // Открытие файла на запись
            using (StreamWriter sw = new StreamWriter("file_in.txt"))
            {
                // Запись заголовков колонок
                sw.WriteLine("lc\tl1x\tl2x\tlot\tbp\trmax\trmin\tmpr\tmg0\thgruza\tnvil");

                // Запись данных объектов в файл txt
                foreach (var obj in data)
                {
                    sw.WriteLine(obj["lc"] + "\t" + obj["l1x"] + "\t" + obj["l2x"] + "\t" + obj["lot"] + "\t" + obj["bp"] + "\t" + obj["rmax"] + "\t" + obj["rmin"] + "\t" + obj["mpr"] + "\t" + obj["mg0"] + "\t" + obj["hgruza"] + "\t" + obj["nvil"]);
                }
            }
        }
    }

    private static void TxtToJson()
    {
        // Чтение данных из файла
        string[] lines = File.ReadAllLines("file_out.txt");

        if (lines.Length == 0)
        {
            Console.WriteLine("File is empty");
            return;
        }


        // Парсинг данных
        List<Dictionary<string, string>> data2 = new List<Dictionary<string, string>>();
        
        string[] headers = lines[0].Split('\t',' ');
        headers = headers.Where(x => x != "").ToArray();

        Console.WriteLine(headers[8]);
        Console.WriteLine("\n");
        for (int i = 1; i < lines.Length; i++)
        {
            
            string[] values = lines[i].Split('\t',' ');
            Dictionary<string, string> row = new Dictionary<string, string>();

            for (int j = 0; j < headers.Length; j++)
            {
                row.Add(headers[j], values[j]);
            }
            data2.Add(row);
        }

        // Запись данных в файл формата json
        string json = JsonConvert.SerializeObject(data2);
        File.WriteAllText("output.json", json);
    }
}