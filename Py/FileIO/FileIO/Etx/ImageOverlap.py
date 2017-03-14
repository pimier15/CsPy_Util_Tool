from PIL import Image

path1 = r'D:\03JobPro\2017\01_LG\Data\2017_03_Test3_SampleData\TestImg\compare\test\T110_100_50_OverView_SameSize.png'
path2 = r'D:\03JobPro\2017\01_LG\Data\2017_03_Test3_SampleData\TestImg\compare\test\T110_100_50_Proced.png'
path3 = r'D:\03JobPro\2017\01_LG\Data\2017_03_Test3_SampleData\TestImg\compare\test\T110_100_50_Overlay.png'

background = Image.open(path1)
overlay = Image.open(path2)



background = background.convert("RGBA")
overlay = overlay.convert("RGBA")

new_img = Image.blend(background, overlay, 0.5)
new_img.save(path3,"PNG")
