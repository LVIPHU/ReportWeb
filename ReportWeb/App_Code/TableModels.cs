using System.Collections.Generic;

public class TableModels
{
    public int Object_Id { get; set; }
    public string Name { get; set; }
    public List<string> Columns { get; set; }

    public TableModels(string name = "", List<string> columns = null, int object_id = 0)
    {
        Name = name;
        Columns = columns;
        Object_Id = object_id;
    }
}