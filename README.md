# PDFPass

Downloads, documentation and support: **[https://pdfpass.app](https://pdfpass.app)**

## Windows installation

The installer sets up PDFPass for your Windows account without administrator rights. You can choose a folder you can write to and enable **Open with PDFPass** in the PDF context menu.

If you installed an earlier version for all users, uninstall it before switching to the per-user installer. Removing that older installation may require administrator rights.

To use the portable ZIP, extract it to a folder of your choice. Run `kontextove-menu-zaregistruj.cmd` to add the PDF context menu for your account, or `kontextove-menu-odregistruj.cmd` to remove it.

## Command-line usage

`pdfpassc.exe` is included beside `PDFPass.exe` in the portable ZIP and the installer. Run it from that directory or invoke it by its full path. It runs without opening a window or prompting for input.

```powershell
.\pdfpassc.exe --help
.\pdfpassc.exe --version
.\pdfpassc.exe encrypt -i "Original document.pdf" -o "Encrypted document.pdf" --owner-password "example-owner"
.\pdfpassc.exe decrypt -i "Encrypted document.pdf" -o "Restored document.pdf" --password "example-owner"
.\pdfpassc.exe watermark -i "Original document.pdf" -o "Watermarked document.pdf" --text "Dôverné"
.\pdfpassc.exe encrypt -i "Original document.pdf" -o "Encrypted document.pdf" --user-password "example-user" --owner-password "example-owner" --watermark "Dôverné" --force
```

`encrypt` takes an unencrypted input, uses AES-256, encrypts metadata, and grants no optional PDF permissions. Supply at least one password. A user password alone also serves as the owner password. An owner password alone lets readers open the PDF without a password, subject to the document's permissions. `decrypt` requires the owner password of an encrypted PDF. `watermark` takes an unencrypted input and applies diagonal text to every page.

Password flags can expose their values in shell history and process listings. You can supply passwords through the following environment variables. An explicit flag takes precedence over its environment variable. Password values and watermark text must contain at least one non-whitespace character.

| Command | Flag | Environment variable |
| --- | --- | --- |
| `encrypt` | `--user-password` | `PDFPASSC_USER_PASSWORD` |
| `encrypt` | `--owner-password` | `PDFPASSC_OWNER_PASSWORD` |
| `decrypt` | `--password` | `PDFPASSC_PASSWORD` |

The output directory must already exist. Use distinct input and output paths. Add `--force` to replace an existing output file. On success the command prints the output path to stdout. Errors go to stderr. Exit codes are `0` for success, help, or version, `2` for invalid arguments, and `1` for PDF, password, or filesystem failures.
