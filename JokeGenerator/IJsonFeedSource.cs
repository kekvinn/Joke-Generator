namespace JokeGenerator
{
    public interface IJsonFeedSource
    {
        public string GetJsonString();

        public void SetOption(string option);
    }
}