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


title món ăn ở title
mô tả: description
youtube link: yt

các bước ở list allSteps gồm: NumberOfStep số bước
			      StepDesc mô tả của bước
			      StepPathImage đường dẫn BitmapImage

thêm các radio button ở homeScreen là page1, page2, page3 và ô textbox search để tìm kiếm

thêm page:
	số page hiện tại ở textBox: pageTextBox
	số page tối đa ở textblock: pageTextBlock
	nút qua trang tiếp theo ở button: nextPageBtn 
	nút qua trang ở sau: backPageBtn