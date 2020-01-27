namespace CodingInterview.Coding.Stucts.Nodes
{

    public class WeightedNode
    {
        public int To { get; }
        public int Cost { get; }

        public WeightedNode(int to, int cost)
        {
            To = to;
            Cost = cost;
        }
    }
}
