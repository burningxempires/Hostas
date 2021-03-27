# Hostas  
 A Live2D Viewer  
 這是一個用於在Unity建置的App環境測試Live2D模型效果的工具  
 這個工具雖然可以隱藏背景，然後疊加到OBS或是其他直播軟體中，但是並沒有追蹤面部表情的功能  
 
 ## 功能  
 * 將live2d模型放置到指定目錄(App/Live2D Model)，然後打開Hostas來讀取模型  
 * 追蹤滑鼠、設定注視目標  
 * 眨眼、麥克風與嘴型同步 (必須在live2d模型輸出時打開同步選項)  
 * 麥克風收音、pitch變聲、輸出音量調整  
 * 模型縮放、移動、角度  
 * 播放表情、動作(必須使用[live2d官方工具](http://sites.cybernoids.jp/cubism_e/live2dviewer/basic/expressions/setting)輸出)  
 ## 功能表  
 * App 工具  
   * ui scale: 使用者介面的大小  
   * background color: 改變背景顏色  
   * fps: 改變每秒畫面的更新頻率，如果遇到延遲或是cpu負荷過重可以試著調整這個選項  
   * hide background: 隱藏背景，如果要再次顯示背景可以調整background color  
   * dock right: 功能表吸附在右手邊，可以切換吸附在左手邊  
   * double click call: 打開這個選項之後，畫面下方的工具按鈕會消失，如果要呼叫功能表請點擊畫面兩次  
   * reload app: 重新載入工具，模型沒有出現在列表中或是要切換其他模型可以使用這個功能  
 * Transform 變形  
   * model scale: 模型的縮放比例  
   * model angle: 模型的旋轉角度  
   * model position: 模型的位置，也可以藉由拖曳模型來改變模型在畫面中的位置  
 * Look 注視  
   * look target/mouse: 看向錨點或是滑鼠  
   * show look target: 顯示錨點，打開之後畫面會出現閃爍的錨點，可以拖動改變錨點位置  
   * look scale: 注視目標的權重，越大的值影響就越大  
 * Controller 控制器  
   * look controller: 注視控制器，打開這個選項模型才會注視目標(需在live2d中使用Angle相關參數)  
   * mouth controller: 嘴型控制器，打開這個選項模型才會和麥克風的音量同步變形(需在live2d中設定嘴型同步)  
   * blink controller: 眨眼控制器，打開這個選項模型的眼睛會眨動(需在live2d中設定眼型同步)  
   * breath controller: 呼吸控制器，打開這個選項模型的ParamBreath參數會和呼吸同步  
   * 注意控制器會和表情以及動作互相影響  
 * Animation/Expression 動作及表情  
   * reset emo: 重置表情  
   * reset motion: 重置動作  
 * Mic/Voice 麥克風和聲音  
   * pitch: 改變播放的聲音音高  
   * volume: 改變播放的音量  
   * start mic: 啟動麥克風  
   * stay表示麥克風尚未啟用，recording...表示正在使用麥克風收音  
 * 時鐘的圖示用來回復預設參數，可以用在模型消失時置中模型和回到模型原本大小  
 * Hostas會在工具的目錄內產生config.json的設定檔案，你可以備份這個檔案來保存配置
 
## 使用方式 
![](https://github.com/burningxempires/Hostas/raw/main/Assets/Hostas/ui%20ref/1.png "live2d 放置模型的資料夾")  
把模型的檔案放到App/Live2D Model這個資料夾裡面，App就是Hostas的資料夾  

![](https://github.com/burningxempires/Hostas/raw/main/Assets/Hostas/ui%20ref/2.png "live2d 模型目錄")  
像這個樣子，你可以在[live2d官網](https://www.live2d.com/en/download/sample-data/ "live2d的範例模型")找到官方的模型來用於測試  

![](https://github.com/burningxempires/Hostas/raw/main/Assets/Hostas/ui%20ref/3.png "live2d 模型結構")  
以官方的模型為例，資料夾裡面會有這些檔案  

![](https://github.com/burningxempires/Hostas/raw/main/Assets/Hostas/ui%20ref/4.png "icon顯示範例")  
如果想要在選單顯示模型的icon，可以使用和model3.json相同檔名的png檔案  

## 額外事項  
### 快捷鍵  
* 右ctrl+右手邊的數字鍵(1~9)可以當成表情的快捷鍵，ctrl+0是回到原本的表情  
### 版本  
* Unity版本 2019.4.22f1  
* Live2D SDK版本 CubismSdkForUnity-4-r.1  
* 使用其他版本可能會導致模型的表情和動作無法正常顯示  
### 其他  
* Live2D SDK中的CubismFadeController會影響到Unity輸出程執行檔的結果，表情和動作無法正常撥放結束  
* CubismFadeController.cs中，`private void UpdateFade (ICubismFadeState fadeState)` 此處的if `(playingMotions == null || playingMotions.Count <= 1)`，將1改為0可以改善這個現象
