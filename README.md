<p align="center">
  <a href="https://github.com/akesseler/Plexdata.CapacityConverter/blob/master/LICENSE.md" alt="license">
    <img src="https://img.shields.io/github/license/akesseler/Plexdata.CapacityConverter.svg" />
  </a>
  <a href="https://github.com/akesseler/Plexdata.CapacityConverter/releases/latest" alt="latest">
    <img src="https://img.shields.io/github/release/akesseler/Plexdata.CapacityConverter.svg" />
  </a>
  <a href="https://github.com/akesseler/Plexdata.CapacityConverter/archive/master.zip" alt="master">
    <img src="https://img.shields.io/github/languages/code-size/akesseler/Plexdata.CapacityConverter.svg" />
  </a>
  <a href="https://github.com/akesseler/Plexdata.CapacityConverter/wiki" alt="wiki">
    <img src="https://img.shields.io/badge/wiki-docs-orange.svg" />
  </a>
  <a href="https://akesseler.github.io/Plexdata.CapacityConverter" alt="help">
    <img src="https://img.shields.io/badge/help-docs-orange.svg" />
  </a>
</p>

## Plexdata Capacity Converter

The _Plexdata Capacity Converter_ represents a library that provides classes as well as interfaces to be used to convert numbers into their capacity representation.

The software has been published under the terms of _MIT License_.

See the documentation under [https://akesseler.github.io/Plexdata.CapacityConverter](https://akesseler.github.io/Plexdata.CapacityConverter) for an overview.

For a complete class documentation see the Wiki under [https://github.com/akesseler/Plexdata.CapacityConverter/wiki](https://github.com/akesseler/Plexdata.CapacityConverter/wiki).

In section _Examples_ please find some code snippets of recommended usage of this library.

### Examples

The examples in this section demonstrate how to use the _Capacity Formatter_ with different features.

**Auto-format, auto-unit-selection and zero decimal digits**

```
String.Format(new CapacityFormatter(), "{0}", 0); // result: "0 Bytes"
String.Format(new CapacityFormatter(), "{0}", 1234); // result: "1 KB"
String.Format(new CapacityFormatter(), "{0}", 12345); // result: "12 KB"
String.Format(new CapacityFormatter(), "{0}", 123456); // result: "121 KB"
String.Format(new CapacityFormatter(), "{0}", 1234567); // result: "1 MB"
String.Format(new CapacityFormatter(), "{0}", 12345678); // result: "12 MB"
String.Format(new CapacityFormatter(), "{0}", 123456789); // result: "118 MB"
String.Format(new CapacityFormatter(), "{0}", 1234567890); // result: "1 GB"
String.Format(new CapacityFormatter(), "{0}", 12345678901); // result: "11 GB"
String.Format(new CapacityFormatter(), "{0}", 123456789012); // result: "115 GB"
String.Format(new CapacityFormatter(), "{0}", 1234567890123); // result: "1 TB"
String.Format(new CapacityFormatter(), "{0}", 12345678901234); // result: "11 TB"
String.Format(new CapacityFormatter(), "{0}", 123456789012345); // result: "112 TB"
String.Format(new CapacityFormatter(), "{0}", 1234567890123456); // result: "1 PB"
String.Format(new CapacityFormatter(), "{0}", 12345678901234567); // result: "11 PB"
String.Format(new CapacityFormatter(), "{0}", 123456789012345678); // result: "110 PB"
```

**Auto-format, auto-unit-selection, three decimal digits**

```
String.Format(new CapacityFormatter(), "{0:3}", 0); // result: "0 Bytes"
String.Format(new CapacityFormatter(), "{0:3}", 123); // result: "123 Bytes"
String.Format(new CapacityFormatter(), "{0:3}", 1234); // result: "1.205 KB"
String.Format(new CapacityFormatter(), "{0:3}", 12345); // result: "12.056 KB"
String.Format(new CapacityFormatter(), "{0:3}", 123456); // result: "120.562 KB"
String.Format(new CapacityFormatter(), "{0:3}", 1234567); // result: "1.177 MB"
String.Format(new CapacityFormatter(), "{0:3}", 12345678); // result: "11.774 MB"
String.Format(new CapacityFormatter(), "{0:3}", 123456789); // result: "117.738 MB"
String.Format(new CapacityFormatter(), "{0:3}", 1234567890); // result: "1.150 GB"
String.Format(new CapacityFormatter(), "{0:3}", 12345678901); // result: "11.498 GB"
String.Format(new CapacityFormatter(), "{0:3}", 123456789012); // result: "114.978 GB"
String.Format(new CapacityFormatter(), "{0:3}", 1234567890123); // result: "1.123 TB"
String.Format(new CapacityFormatter(), "{0:3}", 12345678901234); // result: "11.228 TB"
String.Format(new CapacityFormatter(), "{0:3}", 123456789012345); // result: "112.283 TB"
String.Format(new CapacityFormatter(), "{0:3}", 1234567890123456); // result: "1.097 PB"
String.Format(new CapacityFormatter(), "{0:3}", 12345678901234567); // result: "10.965 PB"
String.Format(new CapacityFormatter(), "{0:3}", 123456789012345678); // result: "109.652 PB"
```

**Auto-format, unit one preferred, zero decimal digits**

```
String.Format(new CapacityFormatter(), "{0:one}", 0); // result: "0 Bytes"
String.Format(new CapacityFormatter(), "{0:one}", 123); // result: "123 Bytes"
String.Format(new CapacityFormatter(), "{0:one}", 1234); // result: "1 KB"
String.Format(new CapacityFormatter(), "{0:one}", 12345); // result: "12 KB"
String.Format(new CapacityFormatter(), "{0:one}", 123456); // result: "121 KB"
String.Format(new CapacityFormatter(), "{0:one}", 1234567); // result: "1 MB"
String.Format(new CapacityFormatter(), "{0:one}", 12345678); // result: "12 MB"
String.Format(new CapacityFormatter(), "{0:one}", 123456789); // result: "118 MB"
String.Format(new CapacityFormatter(), "{0:one}", 1234567890); // result: "1 GB"
String.Format(new CapacityFormatter(), "{0:one}", 12345678901); // result: "11 GB"
String.Format(new CapacityFormatter(), "{0:one}", 123456789012); // result: "115 GB"
String.Format(new CapacityFormatter(), "{0:one}", 1234567890123); // result: "1 TB"
String.Format(new CapacityFormatter(), "{0:one}", 12345678901234); // result: "11 TB"
String.Format(new CapacityFormatter(), "{0:one}", 123456789012345); // result: "112 TB"
String.Format(new CapacityFormatter(), "{0:one}", 1234567890123456); // result: "1 PB"
String.Format(new CapacityFormatter(), "{0:one}", 12345678901234567); // result: "11 PB"
String.Format(new CapacityFormatter(), "{0:one}", 123456789012345678); // result: "110 PB"
```

**Auto-format, unit one preferred, six decimal digits**

```
String.Format(new CapacityFormatter(), "{0:one6}", 0); // result: "0 Bytes"
String.Format(new CapacityFormatter(), "{0:one6}", 123); // result: "123 Bytes"
String.Format(new CapacityFormatter(), "{0:one6}", 1234); // result: "1.205078 KB"
String.Format(new CapacityFormatter(), "{0:one6}", 12345); // result: "12.055664 KB"
String.Format(new CapacityFormatter(), "{0:one6}", 123456); // result: "120.562500 KB"
String.Format(new CapacityFormatter(), "{0:one6}", 1234567); // result: "1.177375 MB"
String.Format(new CapacityFormatter(), "{0:one6}", 12345678); // result: "11.773756 MB"
String.Format(new CapacityFormatter(), "{0:one6}", 123456789); // result: "117.737569 MB"
String.Format(new CapacityFormatter(), "{0:one6}", 1234567890); // result: "1.149781 GB"
String.Format(new CapacityFormatter(), "{0:one6}", 12345678901); // result: "11.497809 GB"
String.Format(new CapacityFormatter(), "{0:one6}", 123456789012); // result: "114.978095 GB"
String.Format(new CapacityFormatter(), "{0:one6}", 1234567890123); // result: "1.122833 TB"
String.Format(new CapacityFormatter(), "{0:one6}", 12345678901234); // result: "11.228330 TB"
String.Format(new CapacityFormatter(), "{0:one6}", 123456789012345); // result: "112.283296 TB"
String.Format(new CapacityFormatter(), "{0:one6}", 1234567890123456); // result: "1.096517 PB"
String.Format(new CapacityFormatter(), "{0:one6}", 12345678901234567); // result: "10.965166 PB"
String.Format(new CapacityFormatter(), "{0:one6}", 123456789012345678); // result: "109.651656 PB"
```

**Auto-format, unit two preferred, zero decimal digits**

```
String.Format(new CapacityFormatter(), "{0:two}", 0); // result: "0 BiB"
String.Format(new CapacityFormatter(), "{0:two}", 123); // result: "123 BiB"
String.Format(new CapacityFormatter(), "{0:two}", 1234); // result: "1 KiB"
String.Format(new CapacityFormatter(), "{0:two}", 12345); // result: "12 KiB"
String.Format(new CapacityFormatter(), "{0:two}", 123456); // result: "121 KiB"
String.Format(new CapacityFormatter(), "{0:two}", 1234567); // result: "1 MiB"
String.Format(new CapacityFormatter(), "{0:two}", 12345678); // result: "12 MiB"
String.Format(new CapacityFormatter(), "{0:two}", 123456789); // result: "118 MiB"
String.Format(new CapacityFormatter(), "{0:two}", 1234567890); // result: "1 GiB"
String.Format(new CapacityFormatter(), "{0:two}", 12345678901); // result: "11 GiB"
String.Format(new CapacityFormatter(), "{0:two}", 123456789012); // result: "115 GiB"
String.Format(new CapacityFormatter(), "{0:two}", 1234567890123); // result: "1 TiB"
String.Format(new CapacityFormatter(), "{0:two}", 12345678901234); // result: "11 TiB"
String.Format(new CapacityFormatter(), "{0:two}", 123456789012345); // result: "112 TiB"
String.Format(new CapacityFormatter(), "{0:two}", 1234567890123456); // result: "1 PiB"
String.Format(new CapacityFormatter(), "{0:two}", 12345678901234567); // result: "11 PiB"
String.Format(new CapacityFormatter(), "{0:two}", 123456789012345678); // result: "110 PiB"
```

**Auto-format, unit two preferred, six decimal digits**

```
String.Format(new CapacityFormatter(), "{0:two6}", 0); // result: "0 BiB"
String.Format(new CapacityFormatter(), "{0:two6}", 123); // result: "123 BiB"
String.Format(new CapacityFormatter(), "{0:two6}", 1234); // result: "1.205078 KiB"
String.Format(new CapacityFormatter(), "{0:two6}", 12345); // result: "12.055664 KiB"
String.Format(new CapacityFormatter(), "{0:two6}", 123456); // result: "120.562500 KiB"
String.Format(new CapacityFormatter(), "{0:two6}", 1234567); // result: "1.177375 MiB"
String.Format(new CapacityFormatter(), "{0:two6}", 12345678); // result: "11.773756 MiB"
String.Format(new CapacityFormatter(), "{0:two6}", 123456789); // result: "117.737569 MiB"
String.Format(new CapacityFormatter(), "{0:two6}", 1234567890); // result: "1.149781 GiB"
String.Format(new CapacityFormatter(), "{0:two6}", 12345678901); // result: "11.497809 GiB"
String.Format(new CapacityFormatter(), "{0:two6}", 123456789012); // result: "114.978095 GiB"
String.Format(new CapacityFormatter(), "{0:two6}", 1234567890123); // result: "1.122833 TiB"
String.Format(new CapacityFormatter(), "{0:two6}", 12345678901234); // result: "11.228330 TiB"
String.Format(new CapacityFormatter(), "{0:two6}", 123456789012345); // result: "112.283296 TiB"
String.Format(new CapacityFormatter(), "{0:two6}", 1234567890123456); // result: "1.096517 PiB"
String.Format(new CapacityFormatter(), "{0:two6}", 12345678901234567); // result: "10.965166 PiB"
String.Format(new CapacityFormatter(), "{0:two6}", 123456789012345678); // result: "109.651656 PiB"
```

**Unit-one-driven format, zero decimal digits**

```
String.Format(new CapacityFormatter(), "{0:bytes}", 123456789012345678); // result: "123,456,789,012,345,678 Bytes"
String.Format(new CapacityFormatter(), "{0:kb}", 123456789012345678); // result: "120,563,270,519,869 KB"
String.Format(new CapacityFormatter(), "{0:mb}", 123456789012345678); // result: "117,737,568,867 MB"
String.Format(new CapacityFormatter(), "{0:gb}", 123456789012345678); // result: "114,978,095 GB"
String.Format(new CapacityFormatter(), "{0:tb}", 123456789012345678); // result: "112,283 TB"
String.Format(new CapacityFormatter(), "{0:pb}", 123456789012345678); // result: "110 PB"
```

**Unit-one-driven format, four decimal digits**

```
String.Format(new CapacityFormatter(), "{0:bytes4}", 123456789012345678); // result: "123,456,789,012,345,678 Bytes"
String.Format(new CapacityFormatter(), "{0:kb4}", 123456789012345678); // result: "120,563,270,519,868.8262 KB"
String.Format(new CapacityFormatter(), "{0:mb4}", 123456789012345678); // result: "117,737,568,867.0594 MB"
String.Format(new CapacityFormatter(), "{0:gb4}", 123456789012345678); // result: "114,978,094.5967 GB"
String.Format(new CapacityFormatter(), "{0:tb4}", 123456789012345678); // result: "112,283.2955 TB"
String.Format(new CapacityFormatter(), "{0:pb4}", 123456789012345678); // result: "109.6517 PB"
```

**Unit-two-driven format, zero decimal digits**

```
String.Format(new CapacityFormatter(), "{0:bib}", 123456789012345678); // result: "123,456,789,012,345,678 BiB"
String.Format(new CapacityFormatter(), "{0:kib}", 123456789012345678); // result: "120,563,270,519,869 KiB"
String.Format(new CapacityFormatter(), "{0:mib}", 123456789012345678); // result: "117,737,568,867 MiB"
String.Format(new CapacityFormatter(), "{0:gib}", 123456789012345678); // result: "114,978,095 GiB"
String.Format(new CapacityFormatter(), "{0:tib}", 123456789012345678); // result: "112,283 TiB"
String.Format(new CapacityFormatter(), "{0:pib}", 123456789012345678); // result: "110 PiB"
```

**Unit-two-driven format, four decimal digits**

```
String.Format(new CapacityFormatter(), "{0:bib4}", 123456789012345678); // result: "123,456,789,012,345,678 BiB"
String.Format(new CapacityFormatter(), "{0:kib4}", 123456789012345678); // result: "120,563,270,519,868.8262 KiB"
String.Format(new CapacityFormatter(), "{0:mib4}", 123456789012345678); // result: "117,737,568,867.0594 MiB"
String.Format(new CapacityFormatter(), "{0:gib4}", 123456789012345678); // result: "114,978,094.5967 GiB"
String.Format(new CapacityFormatter(), "{0:tib4}", 123456789012345678); // result: "112,283.2955 TiB"
String.Format(new CapacityFormatter(), "{0:pib4}", 123456789012345678); // result: "109.6517 PiB"
```

**Custom-unit-format, zero decimal digits**

```
String.Format(new CapacityFormatter(), "{0:my-size}", 123456789012345678); // result: "123,456,789,012,345,678 my-size"
```

**Custom-unit-format, six decimal digits**

```
String.Format(new CapacityFormatter(), "{0:my-size6}", 123456789012345678); // result: "123,456,789,012,345,678 my-size"
```

