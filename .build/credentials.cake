public class BuildCredentials
{
	public BuildCredentials(string username, string password)
	{
		Username = username;
		Password = password;
	}

	public string Username { get; private set; }
	public string Password { get; private set; }

	public bool HasCredentials
	{
		get
		{
			return !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password);
		}
	}
}
