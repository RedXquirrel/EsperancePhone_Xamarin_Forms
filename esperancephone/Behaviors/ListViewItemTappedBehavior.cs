using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace esperancephone.Behaviors
{
    public class ListViewItemTappedBehavior : BehaviorBase<ListView>
    {
        public static readonly BindableProperty ItemTappedCommandProperty = BindableProperty.Create(
          propertyName: "ItemTappedCommand",
          returnType: typeof(ICommand),
          declaringType: typeof(ListViewItemTappedBehavior),
          defaultValue: null,
          propertyChanged: ItemSelectedCommandChanged);

        private static void ItemSelectedCommandChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var view = (ListViewItemTappedBehavior)bindable;
        }

        public ICommand ItemTappedCommand
        {
            get { return (ICommand)GetValue(ItemTappedCommandProperty); }
            set { SetValue(ItemTappedCommandProperty, value); }
        }
        protected override void OnAttachedTo(ListView bindable)
        {
            bindable.ItemTapped += Bindable_ItemTapped;
            base.OnAttachedTo(bindable);
        }

        private void Bindable_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ItemTappedCommand?.Execute(e.Item);
        }

        protected override void OnDetachingFrom(ListView bindable)
        {
            bindable.ItemTapped -= Bindable_ItemTapped;
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


    }
}
