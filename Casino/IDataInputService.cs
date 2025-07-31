public interface IDataInputService
{
    string GetNonEmptyString( string prompt, string errorMessage );
    int GetPositiveInteger( string prompt, string errorMessage );
}