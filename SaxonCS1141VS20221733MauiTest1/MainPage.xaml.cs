namespace SaxonCS1141VS20221733MauiTest1;

public partial class MainPage : ContentPage
{
    private static string SAXON_LICENSE_DIR_NAME = "SAXON_LICENSE_DIR";

    private static string licenseAsset = @"saxon-license.lic";
    public static string LicenseFile { get; private set; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), licenseAsset);

    public MainPage()
    {
        InitializeComponent();
        if (Environment.GetEnvironmentVariable(SAXON_LICENSE_DIR_NAME) == null ||
            (Environment.GetEnvironmentVariable(SAXON_LICENSE_DIR_NAME) != null && !File.Exists(Path.Combine(Environment.GetEnvironmentVariable(SAXON_LICENSE_DIR_NAME), licenseAsset))))
        {
            CopyAssetToFile(licenseAsset, LicenseFile);
            Environment.SetEnvironmentVariable(SAXON_LICENSE_DIR_NAME, Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));
        }
    }

    private async void CopyAssetToFile(string assetName, string copyPath)
    {
        using (var inputStream = await FileSystem.OpenAppPackageFileAsync(assetName))
        {
            using (var outpuStream = File.OpenWrite(copyPath))
            {
                await inputStream.CopyToAsync(outpuStream);
            }
        }
    }

}

