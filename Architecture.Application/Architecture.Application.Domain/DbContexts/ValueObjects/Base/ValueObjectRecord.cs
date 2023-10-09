using Architecture.Application.Core.Notifications.Notifiable.Notifications;

namespace Architecture.Application.Domain.DbContexts.ValueObjects.Base;

public abstract class ValueObject<T> : DomainNotifiable<T>
{
    protected static bool EqualOperator(ValueObject<T> left, ValueObject<T> right)
    {
        if (ReferenceEquals(left, null) ^ ReferenceEquals(right, null))
        {
            return false;
        }
        return ReferenceEquals(left, null) || left.Equals(right);
    }

    protected static bool NotEqualOperator(ValueObject<T> left, ValueObject<T> right)
    {
        return !(EqualOperator(left, right));
    }

    protected abstract IEnumerable<object> GetAtomicValues();

    public override bool Equals(object obj)
    {
        if (obj == null || obj.GetType() != GetType())
        {
            return false;
        }

        ValueObject<T> other = (ValueObject<T>)obj;
        IEnumerator<object> thisValues = GetAtomicValues().GetEnumerator();
        IEnumerator<object> otherValues = other.GetAtomicValues().GetEnumerator();
        while (thisValues.MoveNext() && otherValues.MoveNext())
        {
            if (ReferenceEquals(thisValues.Current, null) ^
                ReferenceEquals(otherValues.Current, null))
            {
                return false;
            }

            if (thisValues.Current != null &&
                !thisValues.Current.Equals(otherValues.Current))
            {
                return false;
            }
        }
        return !thisValues.MoveNext() && !otherValues.MoveNext();
    }

    public override int GetHashCode()
    {
        return GetAtomicValues()
         .Select(x => x != null ? x.GetHashCode() : 0)
         .Aggregate((x, y) => x ^ y);
    }

    public static bool operator ==(ValueObject<T> one, ValueObject<T> two)
    {
        return EqualOperator(one, two);
    }

    public static bool operator !=(ValueObject<T> one, ValueObject<T> two)
    {
        return NotEqualOperator(one, two);
    }
    // Other utility methods
}
