    public class {{name}} : Grammar {
        public {{name}}(string input, Actions actions) {
            this.input = input;
            this.inputSize = input.Length;
            this.actions = actions;
            this.offset = 0;
            this.cache = new Dictionary<Label, Dictionary<int, CacheRecord>>();
            this.failure = 0;
            this.expected = new List<string[]>();
        }

        public static TreeNode parse(string input, Actions actions) {
            {{name}} parser = new {{name}}(input, actions);
            return parser.parse();
        }

        public static TreeNode parse(string input){
            return parse(input, null);
        }

        private static string formatError(string input, int offset, List<string[]> expected) {
            string[] lines = input.Split('\n');
            int lineNo = 0, position = 0;

            while (position <= offset) {
                position += lines[lineNo].Length + 1;
                lineNo += 1;
            }

            string line = lines[lineNo - 1];
            string message = "Line " + lineNo + ": expected one of:\n\n";

            foreach (string[] pair in expected) {
                message += "    - " + pair[1] + " from " + pair[0] + "\n";
            }

            string number = "" + lineNo;
            while (number.Length < 6) number = " " + number;
            message += "\n" + number + " | " + line + "\n";

            position -= line.Length + 10;

            while (position < offset) {
                message += " ";
                position += 1;
            }
            return message + "^";
        }

        private TreeNode parse(){
            TreeNode tree = _read_{{root}}();
            if (tree != FAILURE && offset == inputSize) {
                return tree;
            }
            if (expected.Count <= 0) {
                failure = offset;
                expected.Add(new string[] { {{{grammar}}}, "<EOF>" });
            }
            throw new ParseError(formatError(input, failure, expected));
        }
    }
