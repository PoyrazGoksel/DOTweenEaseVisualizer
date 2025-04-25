# IceFrost Easing Visualizer

A Unity editor extension that provides visual previews for easing functions (Back, Bounce, Elastic, etc.) directly from the **Tools** menu.

## 📸 UI Preview
![Tool Menu](https://github.com/user-attachments/assets/cac7040f-b5b2-4d63-a3dd-64bdeda96686)
![Tool Menu Button](https://github.com/user-attachments/assets/77eef1cf-3341-442c-be24-fa339be99887)
![PreviewScene](https://github.com/user-attachments/assets/a162ec9a-1088-4441-99c9-e86eadf42362)

## ✨ Features

- Easy-to-use curve visualizer for all classic easing types
- Supports `Back`, `Bounce`, `Circ`, `Cubic`, `Elastic`, `Expo`, `Linear`, `Quad`, `Quart`, `Quint`, and `Sine`
- Useful for animators, tweens, or game designers
- Integrates via Unity's top bar `Tools` menu

## 🧩 Integration

1. Click **Tools > Easing Visualizer**.
2. Select an easing type and watch it animate!

## 🧪 Supported Curves

| Category | In | Out | InOut |
|---------|----|-----|--------|
| Back    | ✅  | ✅  | ✅     |
| Bounce  | ✅  | ✅  | ✅     |
| Circ    | ✅  | ✅  | ✅     |
| Cubic   | ✅  | ✅  | ✅     |
| Elastic | ✅  | ✅  | ✅     |
| Expo    | ✅  | ✅  | ✅     |
| Linear  | ✅  | ✅  | ✅     |
| Quad    | ✅  | ✅  | ✅     |
| Quart   | ✅  | ✅  | ✅     |
| Quint   | ✅  | ✅  | ✅     |
| Sine    | ✅  | ✅  | ✅     |

## 💡 Example Easing Library Use Case (Extra)

```csharp
float val = IceFrostEase.GetValueOnCurve(fromFloat: 0f, toFloat: 1f, tVal: Time.time % 1f, easeType: IceFrostEase.IceFrostEases.CubicInOut);
```

Perfect for previewing DOTween-like tweens or authoring custom animations.

---

**Made with ❤️ by IceFrost Games**
