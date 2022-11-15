namespace DevTools.Data;


public record Base64Encoding(string Input, string Output) : InputOutput(Input, Output);
public record UrlEncode(string Input, string Output) : InputOutput(Input, Output);
public record UrlDecode(string Input, string Output) : InputOutput(Input, Output);
public record Base64Decoding(string Input, string Output): InputOutput(Input, Output);

public record InputOutput(string Input, string Output);