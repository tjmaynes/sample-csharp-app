with import <nixpkgs> {};

stdenv.mkDerivation {
  name = "test-driven-aspdotnetcore";
  buildInputs = [
    dotnet-sdk_5
    postgresql
  ];
  shellHook = ''
  make install_dependencies
  '';
}
