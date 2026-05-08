# PaddleOCR_CS

基于 PaddleOCR 的工业视觉检测桌面应用，使用 C# WinForms 开发。

## 功能概述

- **GigE 工业相机采集**：通过大恒相机 SDK (GxIAPINET) 实现实时图像采集
- **模板匹配定位**：使用 Emgu.CV 模板匹配在采集图像中定位 OCR 检测区域
- **PaddleOCR 文字识别**：集成 PaddleOCR (ONNX 推理) 进行工业字符识别
- **OK/NG 判定**：将 OCR 识别结果与标刻内容比对，输出检测结果
- **二维码识别**：支持 WeChatQRCode 解码（可选）
- **图像预处理**：自适应二值化、腐蚀、膨胀等形态学操作
- **图像存储**：按日期自动归档 OK/NG 图像
- **参数管理**：SQLite 数据库存储所有配置参数，支持产品切换

## 系统架构

```
相机采集 → 模板匹配定位 → ROI 裁剪 → [图像预处理] → OCR 识别 → 结果比对 → 输出信号
                                                              ↓
                                                         QR 码识别（可选）
```

## 技术栈

| 组件 | 技术 |
|------|------|
| UI 框架 | WinForms (.NET Framework 4.7.2) |
| 相机 SDK | GxIAPINET (大恒) |
| 图像处理 | Emgu.CV 4.x (OpenCV C# 封装) |
| OCR 引擎 | PaddleOCR (OcrLiteLib, ONNX Runtime) |
| 二维码 | WeChatQRCode |
| 数据库 | SQLite (System.Data.SQLite) |

## 项目结构

```
├── Program.cs              # 程序入口
├── Main.cs                 # 主窗体（相机连接、结果显示）
├── Setting.cs              # 设置窗体（相机参数、标定、区域选择）
├── Camera.cs               # 相机控制（采集、输出、图像保存）
├── ReadOCR.cs              # OCR 识别（模板匹配、文字识别、结果判定）
├── ReadQR.cs               # 二维码识别
├── ImageProcess.cs         # 图像预处理（二值化、腐蚀、膨胀）
├── GxBitmap.cs             # 相机图像格式转换
├── ImageView.cs            # 图像记录查看器
├── Database.cs             # 配置参数读写（内存字典）
├── ZDatabase.cs            # SQLite 数据库操作
├── Parameters.cs           # 程序初始化（OCR 引擎、QR 引擎、文件路径）
├── GlobalVar.cs            # 全局状态管理
├── IParam.cs               # 参数基类（自动持久化）
├── DiskSpace.cs            # 磁盘空间检查
├── Win32Bitmap.cs          # Win32 Bitmap 结构体定义
├── frmEditText.cs          # 参数编辑对话框
└── app.cfg                 # SQLite 数据库文件
```

## 配置参数

所有参数存储在 `app.cfg` (SQLite) 的 `app_ccd` 表中：

| 参数名 | 说明 |
|--------|------|
| output | 是否启用输出信号 (true/false) |
| imageprocess | 是否启用图像预处理 |
| imagesave | 图像保存策略 (null/ng/all) |
| imagespath | 图像保存路径 |
| lasercontent | 标刻内容（用于 OCR 比对） |
| xstart/ystart/swidth/sheight | 模板匹配搜索区域 |
| xoffset/yoffset/woffset/hoffset | OCR 区域偏移 |
| blocksize/filtersize | 自适应二值化参数 |
| erodex/erodey/dilatex/dilatey | 形态学操作参数 |
| capturedelay | 拍照延时 (ms) |
| readqr | 是否启用二维码识别 (true/false) |

## 标定流程

1. 在设置页连接相机，拍摄校准圆图案
2. 点击"标定"按钮，系统自动检测圆心和半径
3. 根据激光标刻参数计算像素-物理坐标映射比例 (scale)
4. 创建模板特征（Pattern）用于后续模板匹配定位
5. 设定搜索区域和 OCR 检测区域

## 构建要求

- Visual Studio 2019+
- .NET Framework 4.7.2
- 依赖库：Emgu.CV 4.x, System.Data.SQLite, OcrLiteLib, GxIAPINET, WeChatQRCode
- 模型文件需放置在 `Models/` 目录下

## 许可证

MIT License
