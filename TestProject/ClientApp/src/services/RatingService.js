export default async function getRating(postId) {
        const response = await fetch(`/rating/get/${postId}`);
        const rating = await response.json();
        return rating;
}