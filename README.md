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

In section _Formatting_ please find a details description of all possible operators as well as recommendations and hints of how to use formatting.

In section _Examples_ please find some code snippets of recommended usage of this library.

### Formatting

Table below shows all possible format operators as well as their meaning.

| Operator   | Description                                                                                                   |
|:-----------|:--------------------------------------------------------------------------------------------------------------|
| **!**      | Switches off the *calculate* mode. With disabled calculation any value is taken as it is.                     |
| **~**      | Switches on the *intercept* mode. Intercept mode suppresses all decimal digits if they consist of zeros only. |
| **One**    | Finds best fitting calculation and appends unit *one*. Unit *one* represents the short unit (e.g. MB).        |
| **Two**    | Finds best fitting calculation and appends unit *two*. Unit *two* represents the long unit (e.g. MiB).        |
| **Bytes**  | Uses *Bibibyte* calculation and appends unit `Bytes` (unit *one*).                                            |
| **BiB**    | Uses *Bibibyte* calculation but appends unit `BiB` (unit *two*).                                              |
| **KB**     | Uses *Kibibyte* calculation and appends unit `KB` (unit *one*).                                               |
| **KiB**    | Uses *Kibibyte* calculation but appends unit `KiB` (unit *two*).                                              |
| **MB**     | Uses *Mebibyte* calculation and appends unit `MB` (unit *one*).                                               |
| **MiB**    | Uses *Mebibyte* calculation but appends unit `MiB` (unit *two*).                                              |
| **GB**     | Uses *Gibibyte* calculation and appends unit `GB` (unit *one*).                                               |
| **GiB**    | Uses *Gibibyte* calculation but appends unit `GiB` (unit *two*).                                              |
| **TB**     | Uses *Tebibyte* calculation and appends unit `TB` (unit *one*).                                               |
| **TiB**    | Uses *Tebibyte* calculation but appends unit `TiB` (unit *two*).                                              |
| **PB**     | Uses *Pebibyte* calculation and appends unit `PB` (unit *one*).                                               |
| **PiB**    | Uses *Pebibyte* calculation but appends unit `PiB` (unit *two*).                                              |
| **EB**     | Uses *Exbibyte* calculation and appends unit `EB` (unit *one*).                                               |
| **EiB**    | Uses *Exbibyte* calculation but appends unit `EiB` (unit *two*).                                              |
| **ZB**     | Uses *Zebibyte* calculation and appends unit `ZB` (unit *one*).                                               |
| **ZiB**    | Uses *Zebibyte* calculation but appends unit `ZiB` (unit *two*).                                              |
| **YB**     | Uses *Yobibyte* calculation and appends unit `YB` (unit *one*).                                               |
| **YiB**    | Uses *Yobibyte* calculation but appends unit `YiB` (unit *two*).                                              |

> **Recommendation**: With disabled *calculate* mode you should use *intercept* mode switched on.

> **Recommendation**: Class `CapacityConverter` respectively class `CapacityEntity` should not be used directly. Better is a 
usage of class `CapacityFormatter` in combination with for example `String.Format()` as shown in all examples below.

> **Hint**: A used format string can consist of upper or lower cases, because the _Capacity Formatter_ does not take care about 
it. This means in detail that format strings like `{0:one5}` or `{0:One5}` or `{0:ONE5}` would generate the same result.

> **Hint**: The _Capacity Formatter_ does not take care about letter and digit combinations. This means that for example a format string like 
`{0:1m2b}` would end up in unit `MB`. The same applies to the number of decimal digits, because something like `{0:1m2b}` 
would generate `12` decimal digits. 

> **Attention**: Each calculation always takes a multiple of 1024, no matter which unit (short or long) is used.

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

**Auto-format, unit one preferred, four decimal digits, mixed interception**

```
String.Format(new CapacityFormatter(), "{0:one4}", 1073741824m)); // result: "1.0000 GB"
String.Format(new CapacityFormatter(), "{0:one~4}", 1073741824m)); // result: "1 GB"
String.Format(new CapacityFormatter(), "{0:one4}", 2213102268m)); // result: "2.0611 GB"
String.Format(new CapacityFormatter(), "{0:one~4}", 2213102268m)); // result: "2.0611 GB"
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

**Unit-two-driven format, four decimal digits, but no calculation**

```
String.Format(new CapacityFormatter(), "{0:!bib4}", 1152921504606846976m); // result: "1,152,921,504,606,846,976 BiB"
String.Format(new CapacityFormatter(), "{0:!kib4}", 1152921504606846976m); // result: "1,152,921,504,606,846,976.0000 KiB"
String.Format(new CapacityFormatter(), "{0:!mib4}", 1152921504606846976m); // result: "1,152,921,504,606,846,976.0000 MiB"
String.Format(new CapacityFormatter(), "{0:!gib4}", 1152921504606846976m); // result: "1,152,921,504,606,846,976.0000 GiB"
String.Format(new CapacityFormatter(), "{0:!tib4}", 1152921504606846976m); // result: "1,152,921,504,606,846,976.0000 TiB"
String.Format(new CapacityFormatter(), "{0:!pib4}", 1152921504606846976m); // result: "1,152,921,504,606,846,976.0000 PiB"
```

**Unit-two-driven format, four decimal digits, but interception**

```
String.Format(new CapacityFormatter(), "{0:bib~4}", 1152921504606846976m); // result: "1,152,921,504,606,846,976 BiB"
String.Format(new CapacityFormatter(), "{0:kib~4}", 1152921504606846976m); // result: "1,125,899,906,842,624 KiB"
String.Format(new CapacityFormatter(), "{0:mib~4}", 1152921504606846976m); // result: "1,099,511,627,776 MiB"
String.Format(new CapacityFormatter(), "{0:gib~4}", 1152921504606846976m); // result: "1,073,741,824 GiB"
String.Format(new CapacityFormatter(), "{0:tib~4}", 1152921504606846976m); // result: "1,048,576 TiB"
String.Format(new CapacityFormatter(), "{0:pib~4}", 1152921504606846976m); // result: "1,024 PiB"
```

**Unit-two-driven format, four decimal digits, with interception, but no calculation**

```
String.Format(new CapacityFormatter(), "{0:!bib~4}", 1152921504606846976m); // result: "1,152,921,504,606,846,976 BiB"
String.Format(new CapacityFormatter(), "{0:!kib~4}", 1152921504606846976m); // result: "1,152,921,504,606,846,976 KiB"
String.Format(new CapacityFormatter(), "{0:!mib~4}", 1152921504606846976m); // result: "1,152,921,504,606,846,976 MiB"
String.Format(new CapacityFormatter(), "{0:!gib~4}", 1152921504606846976m); // result: "1,152,921,504,606,846,976 GiB"
String.Format(new CapacityFormatter(), "{0:!tib~4}", 1152921504606846976m); // result: "1,152,921,504,606,846,976 TiB"
String.Format(new CapacityFormatter(), "{0:!pib~4}", 1152921504606846976m); // result: "1,152,921,504,606,846,976 PiB"
```

