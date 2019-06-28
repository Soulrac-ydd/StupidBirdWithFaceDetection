using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class JsonFaceRequestData
{
    public int error_code { get; set; }

    public string error_msg { get; set; }

    public long log_id { get; set; }

    public long timestamp { get; set; }

    public int cached { get; set; }

    public JsonFaceData result { get; set; }
}

public class JsonFaceData
{
    public int face_num { get; set; }

    public List<JsonFaceList> face_list { get; set; }
}

public class JsonFaceList
{
    public string face_token { get; set; }

    public Location location { get; set; }

    public double face_probability { get; set; }

    public Angle angle { get; set; }

    public Liveness liveness { get; set; }

    public double age { get; set; }

    public long beauty { get; set; }

    public Eye_status eye_status { get; set; }

    public JObject landmark150 { get; set; }
}

public class Location
{
    public double left { get; set; }

    public double top { get; set; }

    public double width { get; set; }

    public double height { get; set; }

    public long rotation { get; set; }
}

public class Angle
{
    public double yaw { get; set; }

    public double pitch { get; set; }

    public double roll { get; set; }
}

public class Liveness
{
    public double livemapscore { get; set; }
}

public class Eye_status
{
    public double left_eye { get; set; }

    public double right_eye { get; set; }
}
