
using Invi_2._0.Data;
using Invi_2._0.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ColorMine.ColorSpaces;
using System;

namespace Invi_2._0.ViewModels
{
    class MainDeviceViewModel : INotifyPropertyChanged
    {
        private PostQuerry PostQuerry;

        private Root _root = GetQuerry.UpdateDeviceList();
        public Root Root
        {
            get
            {
                return _root;
            }
            set
            {
                _root = value;
                OnPropertyChanged("Root");
            }
        }
       
        private Device _selectedDevices;
        public Device SelectedDevices 
        {
            get
            {
                return _selectedDevices;
            }
            set
            {
                _selectedDevices = value;
                OnPropertyChanged("Device");
            }
        
        }

        private Room _selectedRoom;
        public Room SelectedRoom
        {
            get
            {
                return _selectedRoom;
            }
            set
            {
                _selectedRoom = value;
                OnPropertyChanged("Root");
            }
        }

        private SolidColorBrush _selectedColor;
        public SolidColorBrush SelectedColor
        {
            get
            {
                return _selectedColor;
            }
            set
            {
                _selectedColor = value;
                OnPropertyChanged("SelectedColor");
            }
        }

        private int _temperatureK;
        public int TemperatureK
        {
            get
            {
                return _temperatureK;
            }
            set
            {
                _temperatureK = value; 
                OnPropertyChanged("SelectedColor");
            }
        }

        public MainDeviceViewModel()
        {
            PostQuerry = new PostQuerry();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }

        public ICommand OnOffLamp
        {
            get
            {
                return new DelegateСommand(async (obj) =>
                {
                    if (!(bool)SelectedDevices.capabilities[0].state.value)
                    {
                        await Task.Factory.StartNew(() =>
                        {
                            PostQuerry.Off(SelectedDevices.id);
                            OnPropertyChanged("Root");
                        });
                    }
                    else
                    {
                        await Task.Factory.StartNew(() =>
                        {
                            PostQuerry.On(SelectedDevices.id);
                            OnPropertyChanged("Root");
                        });
                    }
                   
                });
            }
        }

        public ICommand OnOffRoom
        {
            get
            {
                return new DelegateСommand(async (obj) =>
                {
                    if(SelectedRoom != null)
                    {
                        if (!(bool)SelectedRoom.state)
                        {
                            await Task.Factory.StartNew(() =>
                            {
                                for (int i = 0; i < SelectedRoom.devices.Count; i++)
                                {
                                    PostQuerry.Off(SelectedRoom.devices[i]);
                                    OnPropertyChanged("Root");
                                }
                               
                            });
                        }
                        else
                        {
                            await Task.Factory.StartNew(() =>
                            {
                                for (int i = 0; i < SelectedRoom.devices.Count; i++)
                                {
                                    PostQuerry.On(SelectedRoom.devices[i]);
                                }
                                OnPropertyChanged("Root");
                            });
                        }
                    }
                    OnPropertyChanged("Root");
                });
            }
        }

        public ICommand ConfirmChangeColor
        {
            get
            {
                return new DelegateСommand(async (obj) =>
                {
                    var rgb = new Rgb { R = SelectedColor.Color.R, G = SelectedColor.Color.G, B = SelectedColor.Color.B };
                    var hsv = rgb.To<Hsv>();

                    int H = Convert.ToInt32(hsv.H);
                    int S = Convert.ToInt32(hsv.S * 100);
                    int V = Convert.ToInt32(hsv.V * 100);

                    if (SelectedDevices != null)
                    {
                        await Task.Factory.StartNew(() =>
                        {
                            PostQuerry.ColorSwap(H, S, V, SelectedDevices.id);
                        });
                    }

                    OnPropertyChanged("SelectedColor");
                });
            }
        }

        public ICommand TemperatureSwap
        {
            get
            {
                return new DelegateСommand(async (obj) =>
                {
                    if (SelectedDevices != null)
                    {
                        await Task.Factory.StartNew(() =>
                        {
                            PostQuerry.TemperatureSwap(TemperatureK, SelectedDevices.id);
                        });
                    }
                    OnPropertyChanged("SelectedColor");
                });
            }
        }

    }
}

