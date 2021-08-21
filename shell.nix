with import <nixpkgs> {};

stdenv.mkDerivation {
  name = "test-driven-aspdotnetcore";
  buildInputs = [
    dotnet-sdk_5
    dotnetCorePackages.aspnetcore_5_0
    postgresql
  ];
  shellHook = ''
  make install_dependencies
  '';
}
