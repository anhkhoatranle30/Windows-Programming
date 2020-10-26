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