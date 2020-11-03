to do list final:
	xử lý link youtube
	độ ưu tiên search




// Functional //

to do list
//function
(V)sửa hàm search
parse youtube 
extra : nhiều step img
//UI
(d)thêm nút back trong detail
(d)chỉnh hiện món ăn detail
(d)click hiện detail trong fav
(d)xử lý chuỗi rỗng
(d)số món ăn mỗi trang trong setting 6 8 10 12(noPages)

CÒN:
	video món ăn
	chỉnh PpP
	làm detail đẹp dễ nứng

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