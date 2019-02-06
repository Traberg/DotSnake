namespace DotSnake.Logic
{
    internal class Result
    {
        public Result(bool didEat, bool died)
        {
            DidEat = didEat;
            Died = died;
        }

        public bool DidEat { get; private set; }
        public bool Died { get; private set; }
    }
}