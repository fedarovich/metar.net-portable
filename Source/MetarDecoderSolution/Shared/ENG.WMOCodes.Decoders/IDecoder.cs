namespace ENG.WMOCodes.Decoders
{
    public interface IDecoder<out T>
    {
        T Decode(string source);
    }
}
