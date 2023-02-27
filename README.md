# Bob Super Stores

<img src='bob.png' align="left"> Bob is our plushy little teddy colleague at [OPTANO](https://optano.com/en/welcome-to-optano/). When Bob is around, there's always fun to be had. He enjoys following trends, he is up for any challenge and just loves doing crazy things. His professional interests are mainly software development and solving operations research problems. Combining both gets him really excited. How can he solve a problem by writing a software solution? For his *Super Stores* he wants to know how he can reduce transportation costs when shipping teddy bears from different warehouses to the stores. Run this sample to find out.
<br />
<br />
Want to get to know Bob better? Follow [#BobAtOPTANO](https://www.linkedin.com/feed/hashtag/bobatoptano&trk=organization_guest_main-feed-card-text) on LinkedIn to dive into Bob's World.

## Running the Application

The application is a simple C# console application. Checkout or download the repository and either run it from an IDE or terminal (requires an installed [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)).

```dotnetcli
dotnet run --project BobSuperStores/BobSuperStores.csproj -- -s CsvData
```

### Options

* `-s|--SourceFileDirectory <Directory>`

    Specifies the directory where the data files should be read from.

### Import Format

Of course you can use any other path containing csv data for the import. The expected input of the folder are the following two files with an input like given in the examples.

- SuperStore.csv

  ```csv
  Name;Longitude;Latitude
  Paderborn;8.698393;51.727691
  Frankfurt;8.682127;50.110924
  ```

- Warehouse.csv

  ```csv
  Name;Longitude;Latitude;OpeningCosts
  F120;5.047412109;51.10351093;1500000
  F119;0.630908203;47.42064245;1500000
  ```

## Running the Tests

This application uses [NUnit](https://nunit.org/) as its unit-testing framework. You can run the tests directly from your IDE or terminal.

```dotnetcli
dotnet test BobSuperStores.Tests/BobSuperStores.Tests.csproj
```

## See Also

> OPTANO Modeling is the **smart module of your software**. It is the **.NET API** which enables **mathematical programming** in your software.
>
> -- <cite>https://optano.com/en/products/optano-modeling/</cite>

OPTANO Modeling is used to create a type-safe optimization model and pass it to a solver.

OPTANO Modeling is available free-of-charge. Get more information at https://optano.com/en/products/optano-modeling.
