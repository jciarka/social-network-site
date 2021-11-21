import React from 'react'

const CommentBubble = ({comment}) => {

    return (
    <div className="comment-bubble">
        <div className="d-flex comment-bubble-header">
            <label>
                <strong>{comment.account.firstname + ' ' + comment.account.lastname }</strong>
            </label>
                   
            <label>{comment.commentDate && comment.commentDate.substring(0,16).replace('T', ' ')}</label>
        </div>

        <div className="comment-bubble-content">
            { comment.text }
        </div>
    </div>
    )
}

export default CommentBubble
