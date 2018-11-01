namespace ProjetoANFCore.Contracts
{
    interface IApiHelper
    {
        void FromService(string baseAdress, string uri);
        void WithJsonContent();
        void WithXMLContent();
    }
}
