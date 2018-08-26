# Project.K.B.C.2018

## 本プロジェクトについて

基本的には、このチーム『ProjectKBC』のURLの公開を禁止します。  
例外で、就活に用いる場合に限ってはOKとします。  

## コーディング規約

### 命名規則

- `UpperCamel`,`lowerCamel`を基本とします。
  - lowerCamelに該当するもの
    - `パラメータ`, `privateなフィールド`, `ローカル変数`
  - UpperCamelに該当するもの
    - 上記以外
- 原則、名前の省略は禁止します
  - 省略はスコープの短い変数であれば許可します
- 配列・リストなどは複数形とします
- `SE`や`OK`といった一般的に2文字から成る頭字語は許容します
  - 例：`SeClip`, `OkButton`

- 名前空間
  - UpperCamel
  - 一意であること
  - 例：`Kbc.Game.RiaManager`
- 実行ファイル / DLLファイル
  - 実行ファイルはプロジェクト名
  - DLLファイルは名前空間およびプロジェクト名
- リソース
  - UpperCamel
- ファイル名
  - クラス名と同一
- クラス
  - UpperCamel
  - 処理中心のクラス
    - 接尾辞として`-er`をつける
  - 派生クラス
    - 継承元のクラスの名前を含める
- フィールド（インスタンスフィールド）
  - privateは**lowerCamel**
  - public, internalは原則使わない
    - プロパティを使う
  - 論理値を格納する場合は`is-`,`has-`,`can-`を用いる
  - 例：
    ```
    [SerializeField]
    private float maxHitPoint;
    
    private float hitPoint;
    public float HitPoint { get { return hitPoint; } }
    
    private bool isActive;
    ```
- 静的フィールド（クラスフィールド）
  - staticなフィールドはUpperCamel
  - 例：`private static readonly Vector3 VectorOne = Vector3.one;`
- フィールド（インスタンスフィールド）のキャッシュ
  - ComponentやGameObject,Transformなどのキャッシュとして扱う変数について
    接尾辞として`-_`をつけてもよい
    ただし、2つの形式が混ざらないようにすること（表記ゆれのないように）
    - 例：
      ```
      private Gameobject gameObject_;
      private Transform transform_;
      private Canvas canvas;
      ```
- プロパティ
  - UpperCamel
- メソッド
  - UpperCamel
    - インスタンス/GameObjectを生成する `Create-` or `New-`
    - 論理値を返す `Is<形容詞>`, `Has<過去分詞>`, `Can<動詞>'
    - 型変換 `To-`
- Parameters / Arguments (仮引数 / 引数)
  - lowerCamel
  - 名前と型で使い方がわかる命名にすること
  - ジェネリック型のパラメータには接尾辞として `-T`
  - 接尾辞に `_-` をつけてもよい ただし、2つの軽視が混ざらないようにすること（表記ゆれのないように）
- ローカル変数
  - lowerCamel
  - できる限り最小のスコープにすること
  - 使用する直前に宣言すること
  - ローカル変数では、スコープが短い場合に名前を省略することを許容します
    - ただし、意味のわかるように効果的に省略するように
- 定数
  - const(コンパイル時定数)はできる限り使用しない
  - static readonly(実行時定数)をなるべく使うこと
- 構造体
  - UpperCamel
- 列挙体
  - 列挙体, 列挙値ともにUpperCamel
- 抽象クラス
  - UpperCamel
  - 接尾辞として`-Bace`をつける
- インターフェイス
  - UpperCamel
  - 接頭辞として`I-`をつける
  - 機能を定義したものには接尾辞として`-able`
  - 出来る限り抽象クラスを使うようにする
- デリゲート
  - UpperCamel
  - 接尾辞として`-Callback`をつける
  - イベント用には接尾辞として`-EventHandler`をつける

### コーディング規約

- レイアウト
  - 1つの行には1つの宣言のみ
  - 1つの行には1つのステートメントのみ
  - タブは使わず、スペース4つ
- コメント
  - コードの末尾は禁止する
  - 別の行に記述する
  - コメント記号と文字の間は空白を1つ挿入する
    - 例：
      ```
      //ダメなコメント
      // 良いコメント
      ```

### その他

- ifの{}は省略しない
  ```
  // ダメな例
  if (!isActive) return;

  // 良い例

  // とくにバリアは、このように1行で書くといい
  if (!isActive) { return; }

  if (!isActive)
  {
    return;
  }
  ```
- 否定形の名前は避ける
  - ```
    // ダメな例
    // 二重否定になってしまう
    if (!isNotFound) { ... }

    // 良い例
    if (!Exist) { ... }
    ```
- マジックナンバーは使わない
  - 原則マジックナンバーは禁止
    - 代わりに正しい名前を持つ変数を用意する
  - 文脈川意味が**明解**であれば例外とする
    - 例：
      ```

      ```
- 比較演算子の向き
  - 左を小、右を大として、`<`,`<=`のみで表現した方がわかりやすい
    - 例：
      ```
      if (xpos >= 0 && xpos <= 180 && ypos >= 0 && ypos <= 180)
      {
        ...
      }

      if (0 <= xpos && xpos <= 180 &&
          0 <= ypos && ypos <= 180)
      {
        ...
      }
      ```
- LINQを使うと意味が明確になる
  - LINQを使うとループ式よりも意味が明確になる
    - 例：
    ```
    var oddMax = 0;
    for (var i = 0; i < values; ++i)
    {
      if (values[i] % 2 == 0)
      {
        if (oddMax < values[i])
        {
          oddMax = values[i];
        }
      }
    }
    
    var oddMax = 0;
    foreach (var value in values)
    {
      if (value % 2 == 0)
      {
        if (oddMax < value)
        {
          oddMax = value;
        }
      }
    }

    var oddMax = values
        .Where(d => (d % 2 == 0))
        .Max();

    var oddMax = (
      from value in values
      where (value % 2 == 0)
      select value).Max();
    ```
- ガードは積極的に使う
  - メソッドのはやい段階での`return`は効果的
  - ネストを減らすことができる
  - 例：
    ```
    private void Update()
    {
      if (!isActive) { return; }
      if (Instance != this) { return; }

      ...
    }
    ```
- 引数には`_-`をつける
  - `this.`を省略しても`private`なフィールドであることが判別できる
  - 引数とフィールドが同名になることを避けられます
- this.およびClassName.について
  - `this.`や`ClassName.`をつけることで明示することができる
  - つけることを推奨するが、冗長な場合は省略してもよい
- var
  - 厳密な型が重要ではない場合は、積極的に`var`を使う
  - 変数名から`var`の型が推測できない場合は変数名を修正する
  - 型を明示した方が間違いが減らせる場合などを考慮して適宜使うこと
- Dispose()の後はnullを設定する
  - `Dispose()`をしてもすぐにメモリから解放されるわけではない
  - nullを設定することで、危険な参照を回避できる 

### ReSharperの導入

Visual Studio for Macは対応していません。  
諦めるか、『JetBrains Rider』を使用してください。  

##### 『JetBrains Rider』を導入する場合

JetBrainsの学生アカウントを作って、以下のリンクからダウンロード/インストールしてください。  
[Rider for Unity](https://www.jetbrains.com/dotnet/promo/unity/)  

#### インストールと設定

1. JetBrainsの学生アカウントを作る  
   参考：[JetBrainsの学割の申請方法について](https://qiita.com/tetrapod117/items/92f965cf1928739b70e4)  
2. ReSharperをインストールする  
   []()  
3. 「StyleCop by JetBrains」を導入する
  - [ReSharper] -> [Extention Manager]
  - 「StyleCop」で検索し、「StyleCop by JetBrains」をインストール

#### 設定


### StyleCopの導入

#### インストール

以下のリンクを参考にインストールをおこなってください。  
[Unity Tutorial: How to Set Up StyleCop](https://blog.redbluegames.com/how-to-set-up-stylecop-in-unity-b3ca908211d9) 

#### 設定

## ディレクトリ構成

## SourceTreeの導入

## GitFlow

- GitFlowを採用

## Gitに関するQ&A

- margeするとき

- ブランチ名
