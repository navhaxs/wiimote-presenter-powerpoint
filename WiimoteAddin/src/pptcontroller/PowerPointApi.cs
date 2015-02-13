using NetOffice;

using Office = NetOffice.OfficeApi;
using NetOffice.OfficeApi.Enums;
using PPt = NetOffice.PowerPointApi;
using NetOffice.PowerPointApi.Enums;


namespace WiimoteAddin
{
    public class PowerPointApi
    {
        // Instead of using sendkeys (previous method in old WiimoteAddin versions),
        // use the COM api...
        // Unfortunately there are many cases to test for... lots of COMExceptions at runtime!
        
        // MSDN references: https://code.msdn.microsoft.com/office/How-to-Automate-control-8c8319b2
        // and the MSDN sample 'VBAutomationControlPPT'
        // Adapted to NetOffice in an attempt to increase interoperability between different PowerPoint versions
        
        // Define PowerPoint Application object
        PPt.Application pptApplication = WiimoteAddin.Connect.thisPowerPoint;
        // Define Presentation object
        PPt.Presentation presentation;
        // Define Slide collection
        PPt.Slides slides;
        PPt.Slide slide;

        // Slide count
        int slidescount;
        // slide index
        int slideIndex;

        public PowerPointApi(PPt.Application thisPPtInstance)
        {
            pptApplication = thisPPtInstance;
        }

        private void getPPtData() {
            
            if (pptApplication != null)
            {
                // Get Presentation Object
                presentation = pptApplication.ActivePresentation;
                // Get Slide collection object
                slides = presentation.Slides;
                // Get Slide count
                slidescount = slides.Count;
                // Get current selected slide 
                try
                {
                    // Get selected slide object in normal view
                    slide = slides[pptApplication.ActiveWindow.Selection.SlideRange.SlideNumber];
                }
                catch
                {
                    // Get selected slide object in reading view
                    slide = pptApplication.SlideShowWindows[1].View.Slide;
                }
            }

        }

        public void NextSlide() {
            System.Diagnostics.Debug.Print("NextSlide called.");
            getPPtData();
            slideIndex = slide.SlideIndex + 1;
            if (slideIndex > slidescount)
            {
                //MessageBox.Show("It is already last page")
            }
            else
            {
                try
                {
                    slide = slides[slideIndex];
                    slides[slideIndex].Select();

                }
                catch
                {
                    pptApplication.SlideShowWindows[1].View.Next();
                    slide = pptApplication.SlideShowWindows[1].View.Slide;
                }
            }
        }

        public void PrevSlide()
        {
            getPPtData();
            slideIndex = slide.SlideIndex - 1;
            if (slideIndex >= 1)
            {
                try
                {
                    slide = slides[slideIndex];
                    slides[slideIndex].Select();
                }
                catch
                {
                    pptApplication.SlideShowWindows[1].View.Previous();
                    slide = pptApplication.SlideShowWindows[1].View.Slide;
                }
            }
            else
            {
                //MessageBox.Show("It is already Fist Page");
            }
        }


        // Transform to First Page
        public void gotoFirst()
        {
            getPPtData();
            try
            {
                // Call Select method to select first slide in normal view
                slides[1].Select();
                slide = slides[1];
            }
            catch
            {
                // Transform to first page in reading view
                pptApplication.SlideShowWindows[1].View.First();
                slide = pptApplication.SlideShowWindows[1].View.Slide;
            }
        }

        // Transform to Last Page

        public void gotoLast()
        {
            getPPtData();
            try
            {
                slides[slidescount].Select();
                slide = slides[slidescount];
            }
            catch
            {
                pptApplication.SlideShowWindows[1].View.Last();
                slide = pptApplication.SlideShowWindows[1].View.Slide;
            }
        }

        public void blankWhite()
        {
            getPPtData();

            //todo
        }

        public void blankBlack()
        {
            getPPtData();
        }

        public void slideshowStart()
        {
            getPPtData();
        }

        public void slideshowStop()
        {
            getPPtData();
        }

        public void showTaskbar()
        {
            getPPtData();
        }
    }
}
