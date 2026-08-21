# ClipboardImageSaver
剪切板图片->桌面文件

## 使用

### 基本用法

- 将生成的 `exe` 放在任意位置（建议加入系统 PATH）。
- 截图或复制图片后，运行 `ClipboardImageSaver`，图片就会以时间戳命名保存到桌面。

### 自定义参数

cmd

```
# 保存到 D:\MyPics，文件名为 myimage.png
ClipboardImageSaver -p D:\MyPics -n myimage

# 保存为 JPG 格式，静默运行（无输出）
ClipboardImageSaver -f jpg --silent
```



### 绑定快捷键（无感触发）

- 创建该 exe 的快捷方式，在“属性”中设置快捷键（如 `Ctrl+Alt+V`）。

- 或者使用 AutoHotkey 脚本：

  autohotkey

  ```
  ^!v::Run "C:\path\to\ClipboardImageSaver.exe --silent"
  ```

  

------

## 🧪 注意事项

- **剪贴板权限**：该工具需要读取剪贴板，Windows 不会拦截，正常使用。
- **STAThread**：代码已标记 `[STAThread]`，确保剪贴板操作正确。
- **图片格式**：默认 PNG，也可指定 JPG/BMP/GIF。
- **文件名冲突**：如果指定了固定文件名且文件已存在，会直接覆盖（可自行扩展增加防冲突逻辑）。

------

## 📥 从 GitHub Action 下载产物

每次 Action 运行成功后，你可以在仓库的 **Actions** 页面找到对应的 workflow，在 **Artifacts** 区域下载 `ClipboardImageSaver.zip`，解压即可得到 `exe` 文件。

