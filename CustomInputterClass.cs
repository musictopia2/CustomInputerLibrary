namespace CustomInputerLibrary;
public class CustomInputterClass : InputFormatter
{
    public async override Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context)
    {
        try
        {
            var thisRequest = context.HttpContext.Request;
            var thisContent = context.HttpContext.Request.ContentType;
            string results;
            if (thisContent == "text/plain; charset=utf-8")
            {
                using StreamReader thisRead = new StreamReader(thisRequest.Body);
                results = await thisRead.ReadToEndAsync();
                return await InputFormatterResult.SuccessAsync(results);
            }
            return await InputFormatterResult.FailureAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message); //to give hints first.
            throw;
        }
    }
    public override bool CanRead(InputFormatterContext context)
    {
        var ContentType = context.HttpContext.Request.ContentType;
        if (ContentType == "text/plain; charset=utf-8")
        {
            return true;
        }
        return false;
    }
    public CustomInputterClass()
    {
        SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/plain"));
    }
}