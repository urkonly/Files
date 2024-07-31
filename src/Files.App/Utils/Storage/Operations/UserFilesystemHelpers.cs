// Copyright (c) 2024 Files Community
// Licensed under the MIT License. See the LICENSE.

namespace Files.App.Utils.Storage;

public class UserFilesystemHelpers : DelegateFilesystemHelpers
{
	private const string defaultCode = "ultracopier"; // application or windows or ultracopier

	private readonly IShellPage page;
	private readonly CancellationToken cancellationToken;

	public UserFilesystemHelpers(IShellPage page, CancellationToken cancellationToken) : base(null!)
	{
		this.page = page;
		this.cancellationToken = cancellationToken;

		helper = GetHelper(defaultCode);
	}

	private IFilesystemHelpers GetHelper(string name) => name switch
	{
		"windows" => new ExternFilesystemHelpers(
			new TryFilesystemHelpers(new WindowsFilesystemHelpers(page))
		,	new FilesystemHelpers(page, cancellationToken)
		),
		"ultracopier" => new ExternFilesystemHelpers(
			new TryFilesystemHelpers(new UltracopierFilesystemHelpers(page))
		,	new FilesystemHelpers(page, cancellationToken)
		),
		_ => new FilesystemHelpers(page, cancellationToken),
	};
}
