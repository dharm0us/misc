static void testMethod()
        {
          /*
        C# code for XML Parsing.
        Input = <toast><visual><binding><text> hi </text><text> there </text></binding></visual><actions><action content="hey"></action></actions></toast>
        Output = Values of all <text> elements. Also values of "content" attributes of <action> elements.
        */
            var str =
                "<toast><visual><binding><text> hi </text><text> there </text></binding></visual><actions><action content=\"hey\"></action></actions></toast>";
            var root = XElement.Parse(str);
            var textEntries = from te in root.Elements("visual").Elements("binding").Elements("text") select te.Value;
            var textList = new List<string>();
            var output = "";
            foreach (string te in textEntries)
            {
                textList.Add(te);
            }
            output = String.Join(", ", textList);
            Debug.WriteLine(output);

            var actions = from action in root.Descendants("action")
                          select (string)action.Attribute("content");
            List<string> actionList = new List<string>();
            foreach (string action in actions)
            {
                actionList.Add(action);
            }
            output = String.Join(", ", actionList);
            Debug.WriteLine(output);

        }
