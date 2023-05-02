# RclSample

Razor クラスライブラリの使用サンプルです。

Blazor のテンプレートで作成される Counter と FetchData をクラスライブラリに移してます。
FetchData は付随する WeatherForecastService もクラスライブラリ側に移動します。

このサンプルでは MudBlazor を使用します。テンプレートも MudBlazor のテンプレートを使用します。

最初にソリューションを作成します。
```
dotnet new sln -n SampleApp
dotnet new mudblazor -ho server -o SampleApp
dotnet new razorclasslib -o SampleApp.Counter
dotnet new razorclasslib -o SampleApp.FetchData
dotnet sln add SampleApp
dotnet sln add SampleApp.Counter
dotnet sln add SampleApp.FetchData
```

ソリューションを作成した状態のコミットが [ソリューションの追加](https://github.com/ochiai-naoki/RclSample/commit/2e353297353e328f6b96cb456013bc6533405f6b) です。

MudBlazor のテンプレートの TargetFramework が net6.0 になっていますので net7.0 にして MudBlazor もアップデートします。
ついでにプロジェクト SampleApp.Counter と SampleApp.FetchData にも MudBlazor を NuGet で追加します。
その後、SampleApp に SampleAppp.Counter と SampleApp.FetchData の参照を追加します。
その状態のコミットが [プロジェクトの参照](https://github.com/ochiai-naoki/RclSample/commit/2ae7203f0255e4fa0f0c967ed45442a98183711d) です。

Razor クラスライブラリのテンプレートでは次のファイルが作成されます。
- wwwroot
    - background.png
    - exampleJsInterop.js
- _Imports.razor
- Compenent1.razor
- Component1.rzor.css
- ExampleJsInterop.cs

今回は _Imports.rzor 以外は使用しませんので削除します。
その後、SampleApp プロジェクトから Pages/Counter.razor を SampleApp.Counter プロジェクトに、Data と Pages/FetchData.razor を SampleApp.FetchData プロジェクトに移動させます。

移動後のコミットが [ファイルの移動](https://github.com/ochiai-naoki/RclSample/commit/e2322354f5050a18ecfb2a8613b89b4b4f5b3af1) です。

移動させた Counter.razor や FetchData.razor を Router が見つけられるように App.razor を修正し Router の AdditionalAssemblies に SampleApp.Counter と SampleApp.FetchData のアッセンブリを設定します。

また、WeatherForecast.cs や WeatherForecastService.cs を SampleApp.FetchData に移動させたので それぞれのネームスペースを SampleApp.Data から SampleApp.FetchData.Data に変更します。
それに伴い SampleApp.Data を参照している箇所を SampleApp.FetchData.Data に変更します。

Counter.razor や FetchData.razor は MudBalzor を使用していますので、それぞれのプロジェクトの _Imports.razor に ```@using MudBlazor``` を追加します。

最後に SampleApp の Program.cs で WeatherForecastService をサービスコンテナに追加していますが、SampleApp.FetchData で使用するサービスを一括で登録できるようにして WeatherForecastService の存在を隠蔽します。
SampleApp.FetchData に ServiceExtensions.cs を追加し、IServiceCollection への拡張メソッド AddFetchData() を追加します。
使用する方は ```builder.Services.AddSingleton<WeatherForecastService>();``` から ```builder.Services.AddFetchData();``` に変更します。

修正箇所はコミット [プログラム修正](https://github.com/ochiai-naoki/RclSample/commit/9cdba1a4a2130148c3daa91b6b303563a1a4f178) を見てください。
