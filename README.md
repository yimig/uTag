# uTag

## a multi-format music tag management lib 一个支持多格式的.Net音乐标签管理库

**目前还在开发中**

 ---
 
### feature：

1.  无视音乐格式，统一管理标签。
2. 修改标签直接修改属性即可
3. 使用.Net Standard 1.0进行开发，理论上同时支持.Net Framework和.Net Core（UWP）

### 预计支持的格式：

- [x] id3v2.3(目前只读)
- [x] flac(目前只读)
- [ ] ogg
- [ ] ape
- [ ] id3v2.4
- [ ] m4a
- [ ] dsf
- [ ] aiff
- [ ] wma

### 示例：

1. 要载入音乐文件，请直接向`TagFactory`的`Load()`静态函数载入文件地址，如：
```csharp
var tag = TagFactory.Load(@"H:\开发\样本\tag shooter\Traumerei.flac");
```
2. `Load()`静态函数的重载函数亦支持`FileInfo`，如：
```csharp
FileInfo fileInfo = new FileInfo(@"H:\开发\样本\tag shooter\Traumerei.flac");
var tag = TagFactory.Load(@"H:\开发\样本\tag shooter\Traumerei.flac");
```
实际上，他们在`TagFactory`类中本来就是相互调用的关系。

要读取一个标签，比如标题，可以直接读取返接口的`Title`属性，如：
```csharp
Console.WriteLine("title：" + tag.Title);
```
除了`Title`属性外，改接口亦支持以下属性：

- Artist 表演者名称
- Album 专辑名称
- Year 发行日期
- Format **标签**的的格式
- TrackID 音轨编号
- Genre 流派

注意，若音乐中没有对应的属性信息，则返回空字符串，**不会抛出异常**。

此外，还可以直接读取一幅专辑图片，返回类型为字节数组，若想将其实例化为支持的图片类型（比如`System.Drawing.Image`），可以借助`MemoryStream`类，如：
```csharp
MemoryStream ms=new MemoryStream(tag.Picture);
Image image=Image.FromStream(ms);
```

具体文档将在开发完毕后择日发布。
