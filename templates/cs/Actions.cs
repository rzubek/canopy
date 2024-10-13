    public interface Actions {
    {{#each actions}}
        public TreeNode {{this}}(string input, int start, int end, List<TreeNode> elements);
    {{/each}}
    }

