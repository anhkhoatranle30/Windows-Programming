// Functional //

to do list
//function
(v)sửa hàm search
(v)cho fav lên đầu
(v)add recipe
random splash
binding step vào detail
//UI
thêm nút back trong detail
chỉnh hiện món ăn detail
click hiện detail trong fav
xử lý chuỗi rỗng
số món ăn mỗi trang trong setting 4 6 8 (noPages)

deadline: thứ 2 tối

 
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


Debug.WriteLine(allSteps[stepCount - 1].StepPathImage);
Debug.WriteLine(((ImageBrush)addImgBtn.Background).ImageSource);