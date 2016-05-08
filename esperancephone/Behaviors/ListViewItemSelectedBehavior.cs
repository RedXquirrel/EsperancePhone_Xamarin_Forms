using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using esperancephone.Views;
using Xamarin.Forms;

namespace esperancephone.Behaviors
{
    public class ListViewItemSelectedBehavior : BehaviorBase<ListView>
    {
        public static readonly BindableProperty ItemSelectedCommandProperty = BindableProperty.Create(
          propertyName: "ItemSelectedCommand",
          returnType: typeof(ICommand),
          declaringType: typeof(ListViewItemSelectedBehavior),
          defaultValue: null,
          propertyChanged: ItemSelectedCommandChanged);

        private static void ItemSelectedCommandChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var view = (ListViewItemSelectedBehavior)bindable;
        }

        public ICommand ItemSelectedCommand
        {
            get { return (ICommand)GetValue(ItemSelectedCommandProperty); }
            set { SetValue(ItemSelectedCommandProperty, value); }
        }
        protected override void OnAttachedTo(ListView bindable)
        {
            bindable.ItemSelected += Bindable_ItemSelected;
            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(ListView bindable)
        {
            bindable.ItemSelected -= Bindable_ItemSelected;
            base.OnDetachingFrom(bindable);
        }

        void OnBindingContextChanged(object sender, EventArgs e)
        {
            OnBindingContextChanged();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            BindingContext = AssociatedObject.BindingContext;
        }

        private void Bindable_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ItemSelectedCommand?.Execute(e.SelectedItem);
        }

    }
}
