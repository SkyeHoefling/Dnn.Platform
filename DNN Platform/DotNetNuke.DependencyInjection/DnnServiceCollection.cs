using Microsoft.Extensions.DependencyInjection;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DotNetNuke.DependencyInjection
{
    internal class DnnServiceCollection : IServiceCollection
    {
        private readonly IList<ServiceDescriptor> _lockedServices;
        private readonly IServiceCollection _services;
        public DnnServiceCollection()
        {
            _lockedServices = new List<ServiceDescriptor>();
            _services = new ServiceCollection();
        }

        public ServiceDescriptor this[int index]
        {
            get => _services[index];
            set => _services[index] = value;
        }

        public int Count => _services.Count;

        public bool IsReadOnly => _services.IsReadOnly;

        public void Add(ServiceDescriptor item)
        {
            _services.Add(item);

            if (IsLockable(item))
                _lockedServices.Add(item);
        }

        public void Clear()
        {
            foreach (var item in _services)
                Remove(item);
        }

        public bool Contains(ServiceDescriptor item)
        {
            return _services.Contains(item);
        }

        public void CopyTo(ServiceDescriptor[] array, int arrayIndex)
        {
            _services.CopyTo(array, arrayIndex);
        }

        // todo - test if we can remove locked services if we get
        // the enumerator
        public IEnumerator<ServiceDescriptor> GetEnumerator()
        {
            return _services.GetEnumerator();
        }

        public int IndexOf(ServiceDescriptor item)
        {
            return _services.IndexOf(item);
        }

        public void Insert(int index, ServiceDescriptor item)
        {
            _services.Insert(index, item);

            if (IsLockable(item))
                _lockedServices.Add(item);
        }

        public bool Remove(ServiceDescriptor item)
        {
            if (_lockedServices.Contains(item))
                return false;

            return _services.Remove(item);
        }

        public void RemoveAt(int index)
        {
            var item = _services[index];
            if (!_lockedServices.Contains(item))
                _services.RemoveAt(index);
        }

        // todo - test if we can remove locked services if we get
        // the enumerator
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _services.GetEnumerator();
        }

        private bool IsLockable(ServiceDescriptor item)
        {
            try
            {
                var attributes = item.ImplementationType.GetCustomAttributes(true);
                if (attributes.Any(x => x is LockedServiceAttribute))
                    return true;
            }
            catch
            {
                return false;
            }

            return false;
        }
    }
}
