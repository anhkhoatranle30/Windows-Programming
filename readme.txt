// Functional //
//Class description
Recipe {
	RecipeID : int
	Title : string
	DesPicture : string, absolute
	Description : string
	Videolink : string
	StepsList : BindingList<Step>
	IsFavorite : bool
}
Step {
	ImgSource : string
	Content: string
}



// UI //
màn hình cho home là homeScreen
tương tự là addScreen, settingScreen, favoriteScreen

có nút saveAddBtn với nút cancelAddBtn

các step nằm trong allSteps gồm cả đường dẫn ở BitmapImage StepPathImage