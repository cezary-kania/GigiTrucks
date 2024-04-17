using GigiTrucks.Services.Newsletter.Domain.Enums;
using GigiTrucks.Services.Newsletter.Domain.Exceptions;
using GigiTrucks.Services.Newsletter.Domain.ValueTypes;

namespace GigiTrucks.Services.Newsletter.Domain.Entities;

public class Newsletter
{
    private const string DefaultNewsletterTitle = "New Newsletter";
    public NewsletterId Id { get; }
    public PublicationStatus Status { get; private set; }
    public PublishAt? PublishAt { get; private set; }
    public NewsletterContent? Content { get; private set; }
    public NewsletterTitle Title { get; private set; } = DefaultNewsletterTitle;
    
    protected Newsletter()
    {
    }

    public Newsletter(
        NewsletterId id,
        PublishAt publishAt,
        PublicationStatus status,
        NewsletterContent content,
        NewsletterTitle title)
    {
        Id = id;
        PublishAt = publishAt;
        Status = status;
        Content = content;
        Title = title;
        
        ValidateTitle();
        if (status is PublicationStatus.ReadyToPublish)
        {
            ValidateMandatoryFieldsBeforePublish();
        }
    }    
    
    public Newsletter(
        NewsletterId id,
        PublicationStatus status)
    {
        Id = id;
        Status = status;

        if (status is PublicationStatus.ReadyToPublish)
        {
            ValidateMandatoryFieldsBeforePublish();
        }
    }

    public void Publish()
    {
        ValidatePublishStatus();
        Status = PublicationStatus.Published;
    }        
    
    public void Schedule(PublishAt publishAt)
    {
        ValidatePublishStatus();
        ValidateMandatoryFieldsBeforePublish();
        PublishAt = publishAt;
        Status = PublicationStatus.ReadyToPublish;
    }

    public void UpdateBody(NewsletterTitle title, NewsletterContent? content)
    {
        ValidatePublishStatus();
        ValidateTitle();
        Title = title;
        Content = content;
        Status = PublicationStatus.Draft;
    }
    
    private void ValidatePublishStatus()
    {
        if (Status is PublicationStatus.Published)
        {
            throw new NewsletterAlreadyPublishedException();
        }
    }    
    
    private void ValidateContent()
    {
        if (string.IsNullOrWhiteSpace(Content))
        {
            throw new CantPublishNewsletterWithEmptyContentException();
        }
    }    
    
    private void ValidateTitle()
    {
        if (string.IsNullOrWhiteSpace(Title))
        {
            throw new NewsletterTitleCantBeEmptyException();
        }
    }    
    
    private void ValidatePublishDate()
    {
        if (PublishAt is null || PublishAt == DateTimeOffset.MinValue)
        {
            throw new PublishDateCantBeEmptyException();
        }
    }
    
    private void ValidateMandatoryFieldsBeforePublish()
    {
        ValidateTitle();
        ValidateContent();
        ValidatePublishDate();
    }
}