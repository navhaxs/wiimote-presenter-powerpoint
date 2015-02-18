using System;

using NetOffice;
using Office = NetOffice.OfficeApi;
using NetOffice.OfficeApi.Enums;
using PPt = NetOffice.PowerPointApi;
using NetOffice.PowerPointApi.Enums;

namespace WiimoteAddin
{
    public class PowerPointApi
    {       
        // References: https://code.msdn.microsoft.com/office/How-to-Automate-control-8c8319b2
        // and the MSDN sample 'VBAutomationControlPPT'

        // adapted to NetOffice
        
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
       
        private void getPPtData()
        {

            try
            {
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
            catch
            {

                // if pptApplication.SlideShowWindows[1].View.Slide is invalid,
                // e.g. the 'Press any key to exit slideshow' screen
                // ignore.

            }
        }

        public void NextSlide() {
            getPPtData();
            slideIndex = slide.SlideIndex + 1;
            if (slideIndex > slidescount)
            {
                //MessageBox.Show("It is already last page")

                // rumble wiimote
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
            try
            {
                if (WiimoteAddin.Win32Api.isPowerPointSlideShowActive())
                {
                    App.ui.DoSendKey("w"); // Dirty hack due to lack of API function:(
                }
            }
            catch
            {

            }
        }

        public void blankBlack()
        {
            try
            {
                if (WiimoteAddin.Win32Api.isPowerPointSlideShowActive())
                {
                    App.ui.DoSendKey("b"); // Dirty hack due to lack of API function:(
                }
            }
            catch
            {

            }
        }

        public void slideshowStart()
        {
            getPPtData();
            try
            {

                if (pptApplication.SlideShowWindows.Count == 0)
                {
                    pptApplication.ActivePresentation.SlideShowSettings.Run();
                }

            }
            catch
            {
            }
        }

        public void slideshowStop()
        {
            getPPtData();
        }

        public void showTaskbar()
        {
            getPPtData();
        }



        internal void zoomIn()
        {
            try
            {
                if (WiimoteAddin.Win32Api.isPowerPointSlideShowActive())
                {
                    App.ui.DoSendKey("{+}"); // Dirty hack due to lack of API function:(
                }
            }
            catch
            {

            }
        }


        internal void zoomOut()
        {
            try
            {
                if (WiimoteAddin.Win32Api.isPowerPointSlideShowActive())
                {
                    App.ui.DoSendKey("-"); // Dirty hack due to lack of API function:(
                }
            }
            catch
            {

            }
        }

    }
}
